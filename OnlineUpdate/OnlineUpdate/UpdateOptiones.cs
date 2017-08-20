using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace OnlineUpdate
{
    /// <summary>
    /// Параметры обновления
    /// </summary>
    public class UpdateOptiones
    {
        /// <summary>
        /// Имя файла журнала подсистемы самообновления
        /// </summary>
        public static readonly string nameFileUpdateLog = "UpdateInstallog.log";
        /// <summary>
        /// Имя приложения инсталятора
        /// </summary>
        public static readonly string appInstallFiles = "UpdateInstaller.exe";
        /// <summary>
        /// Имя файла инструкций к установке
        /// </summary>
        public static readonly string fileInstallInstructiones = "InstallInstructiones.xml";

        /// <summary>
        /// Адресс сервера обновления
        /// </summary>
        public string UrlHostUpdate { get; set; }
        /// <summary>
        /// Путь к временной дирректории системы
        /// </summary>
        public string DirTempFiles { get; set; }
        /// <summary>
        /// Используется сжатие файлов
        /// </summary>
        public bool UseFileCompression { get; set; }
        /// <summary>
        /// Испошльзовать бэкап фалйлов перед установкой новой версии
        /// </summary>
        public bool UseBaclUpFiles { get; set; }

        #region Описание обновляемого приложения

        /// <summary>
        /// Путь к контролируемому приложению
        /// </summary>
        public string PathMonitoredApplication { get; set; }
        /// <summary>
        /// Корневая дирректория обновления
        /// </summary>
        public string RootDirUpdate { get; set; }
        /// <summary>
        /// Текущая версия обновляемого приложения
        /// </summary>
        public Version CurrentVersionUpdateApp
        {
            get
            {
                if (!string.IsNullOrEmpty(PathMonitoredApplication) && File.Exists(PathMonitoredApplication))
                    return Version.Parse(System.Diagnostics.FileVersionInfo.GetVersionInfo(PathMonitoredApplication).FileVersion);
                else
                    return null;
            }
        }
        /// <summary>
        /// Путь к запускемому приложению
        /// </summary>
        public List<RunApplication> RunApplicationes { get; private set; }

        #endregion

        /// <summary>
        /// Адресс файла описания обновления
        /// </summary>
        public string UrlFileDescriptione { get { return UrlHostUpdate + "/UpdateDescription.xml" + FileExtension; } }
        /// <summary>
        /// Локальное рассположение файла описания обновления
        /// </summary>
        public string PatchFileDescriptione { get { return DirTempFiles + "\\UpdateDescription.xml"; } }
        /// <summary>
        /// Путь к логу инсталятора
        /// </summary>
        public string PatchFileInstallog { get; set; }

        /// <summary>
        /// Расширение файла
        /// </summary>
        public string FileExtension
        {
            get
            {
                if (UseFileCompression)
                    return ".bz2";
                else
                    return "";
            }
        }

        public UpdateOptiones(string urlHostUpdate, string dirTempFiles, string rootDirUpdate, string pathMonitoredApplication)
        {
            UseFileCompression = false;
            RunApplicationes = new List<RunApplication>();

            UrlHostUpdate = urlHostUpdate;
            DirTempFiles = dirTempFiles;
            PathMonitoredApplication = pathMonitoredApplication;
            RootDirUpdate = rootDirUpdate;
            PatchFileInstallog = DirTempFiles + "\\" + nameFileUpdateLog;

            RunApplicationes.Add(new RunApplication(pathMonitoredApplication, "", "", false));
        }

        public UpdateOptiones(string urlHostUpdate, string dirTempFiles, string rootDirUpdate, string pathMonitoredApplication, bool useFileCompression)
            :this(urlHostUpdate,  dirTempFiles,  rootDirUpdate,  pathMonitoredApplication)
        {
            UseFileCompression = useFileCompression;
        }

        /// <summary>
        /// Получить содержание тэга
        /// </summary>
        /// <param name="_document"></param>
        /// <param name="_tag"></param>
        /// <returns></returns>
        private string GetInnerTextXML(XmlDocument _document, string _tag)
        {
            if (_document.GetElementsByTagName(_tag).Count > 0)
                return _document.GetElementsByTagName(_tag)[0].InnerText;
            else
                return "";
        }
        /// <summary>
        /// Получить содержание тэга
        /// </summary>
        /// <param name="_document"></param>
        /// <param name="_tag"></param>
        /// <returns></returns>
        private XmlNode GetNodeXML(XmlDocument _document, string _tag)
        {
            if (_document.GetElementsByTagName(_tag).Count > 0)
                return _document.GetElementsByTagName(_tag)[0];
            else
                return null;
        }
        /// <summary>
        /// Получить атрибут узла XML
        /// </summary>
        /// <param name="_node"></param>
        /// <param name="_name"></param>
        /// <returns></returns>
        private string GetAtributeTextXML(XmlNode _node, string _name)
        {
            if (_node.Attributes.GetNamedItem(_name) != null)
                return _node.Attributes.GetNamedItem(_name).InnerText;
            else
                return "";
        }
    }
    /// <summary>
    /// Описываем завершаемое приложение
    /// </summary>
    public class RunApplication
    {
        /// <summary>
        /// Путь к запускаемому приложения
        /// </summary>
        public string LocalPath { get; set; }
        /// <summary>
        /// Имя сервиса
        /// </summary>
        public string NameService { get; set; }
        /// <summary>
        /// Это сервис
        /// </summary>
        public bool IsService { get; set; }
        /// <summary>
        /// Аргументы запуска
        /// </summary>
        public string Argumentes { get; set; }
        /// <summary>
        /// Запустить после установки
        /// </summary>
        public bool IsRun { get; set; }


        /// <summary>
        /// Конструктор класса
        /// </summary>
        public RunApplication(string _localPath, string _argumentes, string _nameService, bool _isService)
        {
            this.LocalPath = _localPath;
            this.IsService = _isService;
            this.NameService = _nameService;
            this.Argumentes = _argumentes;
        }
    }
}
