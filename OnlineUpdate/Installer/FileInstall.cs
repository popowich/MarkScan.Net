using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Installer
{
    /// <summary>
    /// Класс реализует работу с файлом обновления
    /// </summary>
    public class FileInstall
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="_sourceFile"></param>
        /// <param name="_destFile"></param>
        /// <param name="_ourceFileMD5"></param>
        public FileInstall(string _sourceFile, string _destFile)
        {
            this.SourceFile = _sourceFile;
            this.DestFile = _destFile;
            this.NameFile = Path.GetFileName(this.SourceFile);
        }

        /// <summary>
        /// Имя файла
        /// </summary>
        public string NameFile { get; set; }
        /// <summary>
        /// Рассположение копируемого файла
        /// </summary>
        public string SourceFile { get; set; }
        /// <summary>
        /// Рассположение заменяемого файла
        /// </summary>
        public string DestFile { get; set; }

        /// <summary>
        /// Установить файл обновления
        /// </summary>
        /// <returns></returns>
        public void InstalFile()
        {
            string dir = "";
            try
            {
                dir = Path.GetDirectoryName(DestFile);
                //Создаем директорию рассположения файла
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось создать директорию: " + dir, ex);
            }

            try
            {
                if (File.Exists(DestFile))
                {
                    FileInfo fi = new FileInfo(DestFile);
                    if (fi.IsReadOnly)
                        fi.IsReadOnly = false;

                    fi.Delete();
                }

                //Копируем файл с заменой старого файла
                File.Copy(SourceFile, DestFile, true);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка установки файла: " + this.NameFile, ex);
            }
        }
        /// <summary>
        /// Копировать файл 
        /// </summary>
        public void CopyFile(string pathDest)
        {
            string dir = "";
            try
            {
                dir = Path.GetDirectoryName(pathDest);
                //Создаем директорию рассположения файла
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось создать директорию: " + dir, ex);
            }

            try
            {
                if (File.Exists(pathDest))
                {
                    FileInfo fi = new FileInfo(pathDest);
                    if (fi.IsReadOnly)
                        fi.IsReadOnly = false;

                    fi.Delete();
                }

                //Копируем файл с заменой старого файла
                File.Copy(DestFile, pathDest, true);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка копирования файла: " + this.NameFile, ex);
            }

        }
        /// <summary>
        /// Удалить файл источник обновления
        /// </summary>
        public void DeleteFileSource()
        {
            try
            {
                File.Delete(SourceFile);
            }
            catch 
            {
   
            }

        }
    }
}
