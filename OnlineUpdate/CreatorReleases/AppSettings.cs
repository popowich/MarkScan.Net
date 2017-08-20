using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CreatorReleases
{
    /// <summary>
    /// Настройки приложения
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Настройки конфигурации
        /// </summary>
        private static AppSettings settings;
        /// <summary>
        /// Настройки конфигурации
        /// </summary>
        public static AppSettings Settings { get { return settings; } }
        /// <summary>
        /// Путь к файлу настроек
        /// </summary>
        private string fileSettings;

        /// <summary>
        /// Текущая дирректория приложения
        /// </summary>
        public static string DirrApp { get; set; }
        /// <summary>
        /// Текущая рабочая дирректория приложения
        /// </summary>
        public static string WorkDirApp { get; set; }
        /// <summary>
        /// Дирректория хранения релизов
        /// </summary>
        public string BildReleasesDir { get; set; }

        #region Publication settings

        /// <summary>
        /// URL фтп соединения
        /// </summary>
        public string UrlFtp { get; set; }
        /// <summary>
        /// Порт фтп соединения
        /// </summary>
        public int PortFtp { get; set; }
        /// <summary>
        /// Учетная запись пользователя FTP
        /// </summary>
        public string LoginFtp { get; set; }
        /// <summary>
        /// Пароль учетной записи FTP
        /// </summary>
        public string PasswordFtp { get; set; }
        /// <summary>
        /// Каталог публикации
        /// </summary>
        public string PublicationDirFtp { get; set; }

        #endregion

        #region Настройки релиза

        /// <summary>
        /// Дирректория источников файлов релиза
        /// </summary>
        public string CurrDirSourceFilesApp { get; set; }
        /// <summary>
        /// Файлы выпускаемого релиза
        /// </summary>
        public List<FileRelease> SourceFiles { get; set; }
        /// <summary>
        /// Версия релиза
        /// </summary>
        public string CurrVersione { get; set; }
        /// <summary>
        /// Предыдущая версия релиза
        /// </summary>
        public string LastVersione { get; set; }
        /// <summary>
        /// Имя создаваемого каталога релиза
        /// </summary>
        public string NameCreateDirReleas { get; set; }
        /// <summary>
        /// Сжимать файлы релиза
        /// </summary>
        public bool IsCompressFiles { get; set; }
        /// <summary>
        /// Список игнорируемых файлов и папок
        /// </summary>
        public List<IgnorePattern> IgnorePatterns { get;private set; }
        /// <summary>
        /// Список разрешенных файлов и папок
        /// </summary>
        public List<PermitsPattern> PermitsPatterns { get; private set; }

        #endregion

        /// <summary>
        /// Событие исключительной ситуации
        /// </summary>
        public static event ErrorEventHandler ErrorEvent;
        /// <summary>
        /// Вызвать событие
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        public void OnErrorEvent(object _sender, ErrorEventArgs _e)
        {
            if (ErrorEvent != null)
                ErrorEvent(_sender, _e);
        }

        /// <summary>
        /// Статический конструктор
        /// </summary>
        static AppSettings()
        {
            DirrApp = System.Windows.Forms.Application.ExecutablePath.Remove(System.Windows.Forms.Application.ExecutablePath.LastIndexOf('\\'));
            WorkDirApp = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\CreateReleases";
            
            settings = new AppSettings();
        }
        /// <summary>
        /// Конструктор класса
        /// </summary>
        public AppSettings()
        {
            this.fileSettings = WorkDirApp + "\\config.conf";
            this.BildReleasesDir = WorkDirApp + "\\BildReleases";
            this.SourceFiles = new List<FileRelease>();
            this.IgnorePatterns = new List<IgnorePattern>();
            this.PermitsPatterns = new List<PermitsPattern>();
        }

        /// <summary>
        /// Инициализация настроек приложения
        /// </summary>
        public static void InitConfiguration()
        {        
            //Создаем дирректорию приложения
            if (!Directory.Exists(WorkDirApp))
                Directory.CreateDirectory(WorkDirApp);

            if (!Directory.Exists(Settings.BildReleasesDir))
                Directory.CreateDirectory(Settings.BildReleasesDir);

            Settings.LoadSettings();
        }

        /// <summary>
        /// Загрузить настройки приложения
        /// </summary>
        public bool LoadSettings()
        {
            try
            {
                if (File.Exists(this.fileSettings))
                {
                    XmlDocument document = new XmlDocument();
                    document.Load(this.fileSettings);

                    AppSettings.Settings.BildReleasesDir = GetInnerTextXML(document, "BildReleasesDir");
                    this.CurrDirSourceFilesApp = GetInnerTextXML(document, "CurrDirSourceFilesApp");
                    
                    XmlNode elementSourceFiles = GetNodeXML(document, "IgnorePatterns");
                    if (elementSourceFiles != null)
                        foreach (XmlNode nod in elementSourceFiles.ChildNodes)
                        {
                            this.IgnorePatterns.Add(new IgnorePattern(nod.InnerText));
                        }

                    elementSourceFiles = GetNodeXML(document, "PermitsPatterns");
                    if (elementSourceFiles != null)
                        foreach (XmlNode nod in elementSourceFiles.ChildNodes)
                        {
                            this.PermitsPatterns.Add(new PermitsPattern(nod.InnerText));
                        }

                    this.CurrVersione = GetInnerTextXML(document, "CurrVersione");
                    this.LastVersione = GetInnerTextXML(document, "LastVersione");
                    this.NameCreateDirReleas = GetInnerTextXML(document, "NameCreateDirReleas");
                    bool bTemp = false;
                    if (Boolean.TryParse(GetInnerTextXML(document, "IsCompressFiles"), out bTemp))
                        this.IsCompressFiles = bTemp;

                    this.UrlFtp = GetInnerTextXML(document, "UrlFtp");
                    int iTemp = 0;
                    if (int.TryParse(GetInnerTextXML(document, "PortFtp"), out iTemp))
                        this.PortFtp = iTemp;
                    this.LoginFtp = GetInnerTextXML(document, "LoginFtp");
                    this.PasswordFtp = GetInnerTextXML(document, "PasswordFtp");
                    this.PublicationDirFtp = GetInnerTextXML(document, "PublicationDirFtp");
                }
                
                return true;
            }
            catch (Exception ex)
            {
                OnErrorEvent(this, new ErrorEventArgs(ex));
                return false;
            }
        }
        /// <summary>
        /// Сохранить настройки рпиложения
        /// </summary>
        public void SaveSetting()
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><head></head>");

                XmlNode element = document.CreateElement("BildReleasesDir");
                element.InnerText = AppSettings.Settings.BildReleasesDir;
                document.DocumentElement.AppendChild(element);

                element = document.CreateElement("CurrDirSourceFilesApp");
                element.InnerText = this.CurrDirSourceFilesApp;
                document.DocumentElement.AppendChild(element);

                element = document.CreateElement("IgnorePatterns");
                foreach (IgnorePattern file in this.IgnorePatterns)
                {
                    XmlNode childElement = element.AppendChild(document.CreateElement("Pattern"));
                    childElement.InnerText = file.Pattern;
                }
                document.DocumentElement.AppendChild(element);

                element = document.CreateElement("PermitsPatterns");
                foreach (IgnorePattern file in this.PermitsPatterns)
                {
                    XmlNode childElement = element.AppendChild(document.CreateElement("Pattern"));
                    childElement.InnerText = file.Pattern;
                }
                document.DocumentElement.AppendChild(element);

                element = document.CreateElement("CurrVersione");
                element.InnerText = this.CurrVersione;
                document.DocumentElement.AppendChild(element);

                element = document.CreateElement("LastVersione");
                element.InnerText = this.LastVersione;
                document.DocumentElement.AppendChild(element);

                element = document.CreateElement("NameCreateDirReleas");
                element.InnerText = this.NameCreateDirReleas;
                document.DocumentElement.AppendChild(element);

                element = document.CreateElement("IsCompressFiles");
                element.InnerText = this.IsCompressFiles.ToString();
                document.DocumentElement.AppendChild(element);

                element = document.CreateElement("UrlFtp");
                element.InnerText = this.UrlFtp;
                document.DocumentElement.AppendChild(element);

                element = document.CreateElement("PortFtp");
                element.InnerText = this.PortFtp.ToString();
                document.DocumentElement.AppendChild(element);

                element = document.CreateElement("LoginFtp");
                element.InnerText = this.LoginFtp;
                document.DocumentElement.AppendChild(element);

                element = document.CreateElement("PasswordFtp");
                element.InnerText = this.PasswordFtp;
                document.DocumentElement.AppendChild(element);

                element = document.CreateElement("PublicationDirFtp");
                element.InnerText = this.PublicationDirFtp;
                document.DocumentElement.AppendChild(element);

                document.Save(this.fileSettings);
            }
            catch (Exception ex)
            {
                OnErrorEvent(this, new ErrorEventArgs(ex));
            }
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
    }
    /// <summary>
    /// Шаблон запрещеного файла
    /// </summary>
    public class IgnorePattern
    {
        /// <summary>
        /// Оригиал шаблона
        /// </summary>
        private string patternOrigin;
        /// <summary>
        /// Шаблон
        /// </summary>
        private string pattern;

        /// <summary>
        /// Полный путь
        /// </summary>
        public string Pattern 
        {
            get { return this.patternOrigin; }
            set
            {
                this.patternOrigin = value;
                this.pattern = this.patternOrigin.ToLower().Replace("*", "");
            }
        }
       
        /// <summary>
        /// Конструктор класса
        /// </summary>
        public IgnorePattern()
        {

        }
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="_fullName"></param>
        public IgnorePattern(string _fullName)
        {
            Pattern = _fullName;
        }

        public bool Compea(string _name)
        {
            if (this.patternOrigin[0] == '*')
                return _name.ToLower().LastIndexOf(this.pattern) > -1;
            else
                return _name.ToLower() == this.pattern;
        }
    }
    /// <summary>
    /// Шаблон разрешеного файла
    /// </summary>
    public class PermitsPattern : IgnorePattern
    {
        public PermitsPattern()
            :base()
        {

        }

        public PermitsPattern(string _fullName)
            : base(_fullName)
        {

        }
    }
}
