using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using OnlineUpdate.MultithreadedDownload;
using OnlineUpdate.FileInstaller;
using System.Reflection;
using System.Runtime.InteropServices;

namespace OnlineUpdate
{
    /// <summary>
    /// Класс описывает свойства и методы обработки единичного файла обновления
    /// </summary>
    public class FileUpdate
    {
        private IDownloadFile _downloader;

        /// <summary>
        /// Ссылка файла на сервере
        /// </summary>
        public string RelativeUrlFile { get; set; }
        /// <summary>
        /// Имя временного файла
        /// </summary>
        public string NameFileTemp { get; set; }
        /// <summary>
        /// Имя обновляемого файла
        /// </summary>
        public string NameFileInstall { get; set; }
        /// <summary>
        /// Путь к временному рассположению файла
        /// </summary>
        public string PatchFileTemp { get; set; }
        /// <summary>
        /// Локальный путь рассполоения файла
        /// </summary>
        public string PathFileInstall { get; set; }

        /// <summary>
        /// Файл требует специальной устанвоки
        /// </summary>
        public bool SpecialInstallFile { get; set; }
        /// <summary>
        /// Хэш файла
        /// </summary>
        public string MD5 { get; set; }
        /// <summary>
        /// Версия файла
        /// </summary>
        public Version NewVersion { get; set; }
        /// <summary>
        /// Флаг информирующий о том что файл загружен
        /// </summary>
        public bool IsLoaded { get; private set; }
        /// <summary>
        /// Флаг указывавающиы что файл пригоден для обновления
        /// </summary>
        public bool IsFit { get; private set; }
        /// <summary>
        /// Файл успешно установлен
        /// </summary>
        public bool IsInstal { get; private set; }

        public FileUpdate()
        {
            _downloader = UpdateManager._fileDownloader;
        }

        public FileUpdate(string pathFileInstall, string pathDownload, string _relativeLink, string _md5, Version _ver)
            : this()
        {
            this.NameFileTemp = Path.GetFileName(pathDownload);

            if (string.IsNullOrEmpty(UpdateManager.Options.FileExtension))
                this.NameFileInstall = Path.GetFileName(pathFileInstall);
            else
                this.NameFileInstall = Path.GetFileName(pathFileInstall).Replace(UpdateManager.Options.FileExtension, "");

            this.PatchFileTemp = UpdateManager.Options.DirTempFiles + pathDownload.Remove(pathDownload.LastIndexOf('/'));
            this.PathFileInstall = UpdateManager.Options.RootDirUpdate + pathFileInstall.Remove(pathFileInstall.LastIndexOf('\\'));

            this.MD5 = _md5;
            this.RelativeUrlFile = _relativeLink;
            this.NewVersion = _ver;
        }


        /// <summary>
        /// Получить признак необходимости обновить файл
        /// </summary>
        /// <returns></returns>
        public bool GetNeedToUpdateFile()
        {
            bool allowLoad = false;
            //Загружаем файл обновления если такого файла не существует
            if (File.Exists(this.PathFileInstall + "\\" + this.NameFileInstall))
            {
                //Если можно првоерить по версии
                if (this.NewVersion != null)
                {
                    Version currentVersion = AssemblyUtilites.GetVersionAssembly(this.PathFileInstall + "\\" + this.NameFileInstall);
                    //Если версия меньше или = 0 обновлять файл не нужно
                    if (currentVersion != null
                        && this.NewVersion.CompareTo(currentVersion) > 0)
                        allowLoad = true;
                }
                else
                {
                    string currentMD5 = AssemblyUtilites.GetFilesMD5(this.PathFileInstall + "\\" + this.NameFileInstall);
                    if (currentMD5 != this.MD5)
                        allowLoad = true;
                }
            }
            else
                allowLoad = true;

            return allowLoad;
        }
        /// <summary>
        /// Загрузить файл обновления во временную директорию
        /// </summary>
        /// <returns></returns>
        public bool DownloadFile()
        {
            IsLoaded = false;
            IsFit = false;

            //Создаем дирректорию во временном хранилище
            if (!Directory.Exists(this.PatchFileTemp))
                Directory.CreateDirectory(this.PatchFileTemp);

            //Если файл ранее скачан, существует во временной дирректории и хеш его совпадает
            if (File.Exists(this.PatchFileTemp + "\\" + this.NameFileTemp))
            {
                IsLoaded = true;
                //Проверяем целостность файла
                string _md5 = AssemblyUtilites.GetFilesMD5(this.PatchFileTemp + "\\" + this.NameFileTemp);

                if (_md5 == MD5)
                {
                    IsFit = true;
                }
                else
                    File.Delete(this.PatchFileTemp + "\\" + this.NameFileTemp);
            }

            //Три попытки скачивания
            int tryCount = 0;

            while (tryCount <= 2 && IsFit == false)
            {
                try
                {
                    _downloader.DownloadFile(this.RelativeUrlFile, this.PatchFileTemp + "\\" + this.NameFileTemp);

                    //Проверяем целостность файла
                    string _md5 = AssemblyUtilites.GetFilesMD5(this.PatchFileTemp + "\\" + this.NameFileTemp);

                    if (_md5 == MD5)
                    {
                        IsFit = true;
                        IsLoaded = true;
                        break;
                    }
                    else
                        throw new Exception("The downloaded file failed integrity check");
                }
                catch (Exception ex)
                {
                    if (tryCount == 2)
                        throw new Exception("Failed to download the update file" + RelativeUrlFile, ex);
                    else
                        tryCount++;

                    if (File.Exists(this.PatchFileTemp + "\\" + this.NameFileTemp))
                        File.Delete(this.PatchFileTemp + "\\" + this.NameFileTemp);
                }
            }

            //Распаковка файла при необходимости
            if (UpdateManager.Options.UseFileCompression && IsFit)
            {
                if (File.Exists(this.PatchFileTemp + "\\" + this.NameFileInstall))
                    File.Delete(this.PatchFileTemp + "\\" + this.NameFileInstall);

                FileUtilities.DecompressBz2(this.PatchFileTemp + "\\" + this.NameFileTemp, this.PatchFileTemp + "\\" + this.NameFileInstall);
            }

            return IsFit;
        }
        /// <summary>
        /// Установить файл обновления
        /// </summary>
        /// <returns></returns>
        public bool InstalFile()
        {
            IsInstal = false;

            if (!IsLoaded)
                throw new Exception("The file is not downloaded from the update server");

            if (!IsFit)
                throw new Exception("The file is damaged and can not be used to update");

            try
            {
                //Создаем директорию рассположения файла
                if (!Directory.Exists(PathFileInstall))
                    Directory.CreateDirectory(PathFileInstall);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create the directory location of the updated file: " + ex.Message);
            }

            try
            {
                //Удаляем существующий файл
                if (File.Exists(PathFileInstall + "\\" + NameFileInstall))
                {
                    FileInfo fi = new FileInfo(PathFileInstall + "\\" + NameFileInstall);
                    if (fi.IsReadOnly)
                        fi.IsReadOnly = false;

                    fi.Delete();
                }

                //Копируем
                File.Copy(PatchFileTemp + "\\" + NameFileTemp, PathFileInstall + "\\" + NameFileInstall, true);
                IsInstal = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to install the update file: " + RelativeUrlFile, ex);
            }

            return IsInstal;
        }
        /// <summary>
        /// Удалить файл источник обновления
        /// </summary>
        public void DeleteFileSource()
        {
            try
            {
                //Копируем файл с заменой старого файла
                File.Delete(PatchFileTemp + "\\" + NameFileTemp);
            }
            catch
            {

            }

        }
    }
}
