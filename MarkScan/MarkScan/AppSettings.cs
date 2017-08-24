using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace MarkScan
{
    internal class AppSettings
    {
        internal static string NameAssembly
        {
            get
            {
                return System.Reflection.Assembly.GetEntryAssembly().FullName.Substring(0, System.Reflection.Assembly.GetEntryAssembly().FullName.IndexOf(','));
            }
        }

        /// <summary>
        /// Версия сборки
        /// </summary>
        public static string VerAssembly
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        internal static string CurrDir { get; private set; }

        internal static string UserWorkDir { get; set; }

        internal static string UpdateDir { get; private set; }

        internal static string LogsDir { get; private set; }

        internal static string LogFile { get; private set; }

        internal static AppSettings settings;

        internal string Login { get; set; }

        internal string Pass { get; set; }

        static AppSettings()
        {
            CurrDir = System.Windows.Forms.Application.ExecutablePath.Remove(System.Windows.Forms.Application.ExecutablePath.LastIndexOf('\\'));
            UserWorkDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\MarkScanNet";
            UpdateDir = UserWorkDir + "\\TempUpdate";
            LogsDir = UserWorkDir + "\\Logs";

            LogFile = LogsDir + "\\logs.txt";

            settings = new AppSettings();

            try
            {

                //Создаем дирректорию приложения
                if (!Directory.Exists(UserWorkDir))
                    Directory.CreateDirectory(UserWorkDir);
                if (!Directory.Exists(UpdateDir))
                    Directory.CreateDirectory(UpdateDir);

                if (!Directory.Exists(LogsDir))
                    Directory.CreateDirectory(LogsDir);
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        public bool LoadSettings()
        {
            try
            {
                if (File.Exists(CurrDir + "\\settings.conf"))
                {
                    XmlDocument document = new XmlDocument();
                    document.Load(CurrDir + "\\settings.conf");

                    Login = GetInnerTextXML(document, "Login");

                    string temp = "";
                    Tools.Base64.TryParseBase64(Encoding.ASCII, Login,out temp);
                    Tools.Base64.TryParseBase64(Encoding.ASCII, temp, out temp);
                    Login = temp;

                    Pass = GetInnerTextXML(document, "Password");

                    temp = "";
                    Tools.Base64.TryParseBase64(Encoding.ASCII, Pass, out temp);
                    Tools.Base64.TryParseBase64(Encoding.ASCII, temp, out temp);
                    Pass = temp;

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                HandlerException(ex);
                return false;
            }
        }

        public void SaveSetting()
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><head></head>");

                XmlNode element = document.CreateElement("Login");
                element.InnerText = Tools.Base64.ToBase64(Encoding.ASCII, Tools.Base64.ToBase64(Encoding.ASCII, this.Login));
                document.DocumentElement.AppendChild(element);

                element = document.CreateElement("Password");
                element.InnerText = Tools.Base64.ToBase64(Encoding.ASCII, Tools.Base64.ToBase64(Encoding.ASCII, this.Pass));
                document.DocumentElement.AppendChild(element);

                document.Save(CurrDir + "\\settings.conf");
            }
            catch (Exception ex)
            {
                HandlerException(ex);
            }
        }

        private string GetInnerTextXML(XmlDocument _document, string _tag)
        {
            if (_document.GetElementsByTagName(_tag).Count > 0)
                return _document.GetElementsByTagName(_tag)[0].InnerText;
            else
                return "";
        }

        private XmlNode GetNodeXML(XmlDocument _document, string _tag)
        {
            if (_document.GetElementsByTagName(_tag).Count > 0)
                return _document.GetElementsByTagName(_tag)[0];
            else
                return null;
        }

        public static void SaveDebug(string debug)
        {
            if (!string.IsNullOrEmpty(LogFile))
            {
                StreamWriter write = null;
                try
                {
                    write = new StreamWriter(LogFile, true, Encoding.UTF8);
                    write.WriteLine(DateTime.Now + " - Debug: " + debug);
                }
                catch (Exception ex2)
                {

                }
                finally
                {
                    if (write != null)
                        write.Close();
                }
            }
        }

        public static void HandlerException(Exception ex)
        {
            if (!string.IsNullOrEmpty(LogFile))
            {
                StreamWriter write = null;
                try
                {
                    write = new StreamWriter(LogFile, true, Encoding.UTF8);
                    write.WriteLine(DateTime.Now + " - Error: " + ex.Message);
                    write.WriteLine("StackTrace" + ex.StackTrace);

                    if (ex.InnerException != null)
                    {
                        write.WriteLine("              ->  " + ex.InnerException.Message);
                        if (ex.InnerException.InnerException != null)
                        {
                            write.WriteLine("                -> " + ex.InnerException.InnerException.Message);
                            if (ex.InnerException.InnerException.InnerException != null)
                            {
                                write.WriteLine("                  -> " + ex.InnerException.InnerException.InnerException.Message);
                            }
                        }
                    }
                }
                catch (Exception ex2)
                {

                }
                finally
                {
                    if (write != null)
                        write.Close();
                }
            }
        }
    }
}
