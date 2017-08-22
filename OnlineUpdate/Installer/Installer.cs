using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Installer
{
    /// <summary>
    /// Инструкции к установке
    /// </summary>
    public class Installer : IDisposable
    {
        /// <summary>
        /// Путь к лог файлу
        /// </summary>
        public static string LogFile { get; set; }
        /// <summary>
        /// Текущая версия обновляемого приложения
        /// </summary>
        public Version CurrentVersion { get; private set; }
        /// <summary>
        /// Версия обнолвения
        /// </summary>
        public Version UpgradeToVersion { get; private set; }
        /// <summary>
        /// Дата публикации обновления
        /// </summary>
        public DateTime PublicationDate { get; private set; }
        /// <summary>
        /// Описание обновления
        /// </summary>
        public string UpdateDescriptiones { get; private set; }
        /// <summary>
        /// Путь к корневому каталогу обновлений
        /// </summary>
        public string RootDirTemp { get; private set; }
        /// <summary>
        /// Пуь к обновляемому корневому каталогу
        /// </summary>
        public string RootDirUpdate { get; private set; }
        /// <summary>
        /// Путь к каталогу бэкапа
        /// </summary>
        public string RootDirBackUp { get; set; }
        /// <summary>
        /// Испошльзовать бэкап фалйлов перед установкой новой версии
        /// </summary>
        public bool UseBakcUpFiles { get; set; }
        /// <summary>
        /// Показывать окно инсталятора 
        /// </summary>
        public bool ShowWindowProcessInstall { get; set; } = true;

        /// <summary>
        /// Список устанавливаемых файлов
        /// </summary>
        public List<FileInstall> InstallFiles { get; private set; }
        /// <summary>
        /// Путь к запускемому приложению
        /// </summary>
        public List<RunApplication> RunApplicationes { get; private set; }

        /// <summary>
        /// Выполняется установка
        /// </summary>
        public bool IsRunInstalling { get; private set; }
        /// <summary>
        /// Выполнено с ошибками
        /// </summary>
        public bool IsErrors { get; private set; }

        #region Eventes

        /// <summary>
        /// Обработчик начала установки файлов
        /// </summary>
        public event EventHandler<BeginInstallEventArgs> BeginInstallEvent;
        /// <summary>
        /// Обработчик начала устанвоки очередного файла
        /// </summary>
        public event EventHandler<InstallFileEventArgs> BeginInstallFileEvent;
        /// <summary>
        /// Обработчик окончания установки очередного файла
        /// </summary>
        public event EventHandler<InstallFileEventArgs> InstalledFileEvent;
        /// <summary>
        /// Обработчик окончания установки файлов
        /// </summary>
        public event EventHandler EndInstallEvent;
        /// <summary>
        /// Событие при изменении текущего этапа операции
        /// </summary>
        public event EventHandler<ChangingPhaseOperationEventArgs> ChangingPhaseOperationEvent;
        /// <summary>
        /// Обработчик исключительной ситуции 
        /// </summary>
        public event EventHandler<ErrorEventArgs> ErrorEvent;


        /// <summary>
        /// Вызвать обработчик
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        public void OnBeginInstallEvent(Object _sender, BeginInstallEventArgs _e)
        {
            var eventHandler = BeginInstallEvent;
            if (eventHandler != null)
                eventHandler(_sender, _e);
        }
        /// <summary>
        /// Вызвать обработчик
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        public void OnInstalledFileEvent(Object _sender, InstallFileEventArgs _e)
        {
            var eventHandler = InstalledFileEvent;
            if (eventHandler != null)
                eventHandler(_sender, _e);
        }
        /// <summary>
        /// Вызвать обработчик
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        public void OnBeginInstallFileEvent(Object _sender, InstallFileEventArgs _e)
        {
            var eventHandler = BeginInstallFileEvent;
            if (eventHandler != null)
                eventHandler(_sender, _e);
        }
        /// <summary>
        /// Вызвать обработчик
        /// </summary>
        /// <param name="_ex"></param>
        /// <param name="_text"></param>
        public void OnEndInstallEvent(Object _sender, EventArgs _e)
        {
            var eventHandler = EndInstallEvent;
            if (eventHandler != null)
                eventHandler(_sender, _e);
        }
        /// <summary>
        /// Вызвать обработчик
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        public void OnChangingPhaseOperationEvent(Object _sender, ChangingPhaseOperationEventArgs _e)
        {
            var eventHandler = ChangingPhaseOperationEvent;
            if (eventHandler != null)
                eventHandler(_sender, _e);
        }
        /// <summary>
        /// Вызвать обработчик исключительной ситуации
        /// </summary>
        /// <param name="_ex"></param>
        /// <param name="_text"></param>
        public void OnErrorEvent(Object _sender, ErrorEventArgs _e)
        {
            var eventHandler = ErrorEvent;
            if (eventHandler != null)
                eventHandler(this, _e);
        }


        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        public Installer()
        {
            this.InstallFiles = new List<FileInstall>();
            this.RunApplicationes = new List<RunApplication>();
        }

        /// <summary>
        /// Очищаем данные объекта
        /// </summary>
        public void Dispose()
        {
            if (this.BeginInstallEvent != null)
                foreach (EventHandler<BeginInstallEventArgs> d in this.BeginInstallEvent.GetInvocationList())
                    this.BeginInstallEvent -= d;

            if (this.InstalledFileEvent != null)
                foreach (EventHandler<InstallFileEventArgs> d in this.InstalledFileEvent.GetInvocationList())
                    this.InstalledFileEvent -= d;

            if (this.BeginInstallFileEvent != null)
                foreach (EventHandler<InstallFileEventArgs> d in this.BeginInstallFileEvent.GetInvocationList())
                    this.BeginInstallFileEvent -= d;

            if (this.EndInstallEvent != null)
                foreach (EventHandler d in this.EndInstallEvent.GetInvocationList())
                    this.EndInstallEvent -= d;

            if (this.ChangingPhaseOperationEvent != null)
                foreach (EventHandler<ChangingPhaseOperationEventArgs> d in this.ChangingPhaseOperationEvent.GetInvocationList())
                    this.ChangingPhaseOperationEvent -= d;

            if (this.ErrorEvent != null)
                foreach (EventHandler<ErrorEventArgs> d in this.ErrorEvent.GetInvocationList())
                    this.ErrorEvent -= d;
        }

        /// <summary>
        /// Загрузить инструкцию проведения устанвоки
        /// </summary>
        /// <param name="_file"></param>
        public bool LoadInstructiones(string _file)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(_file);

                Version verTemp = null;
                Version.TryParse(GetInnerTextXML(document, "CurrentVersion"), out verTemp);
                this.CurrentVersion = verTemp;

                verTemp = null;
                Version.TryParse(GetInnerTextXML(document, "UpgradeToVersion"), out verTemp);
                this.UpgradeToVersion = verTemp;

                DateTime dtPublicTemp = DateTime.MinValue;
                DateTime.TryParse(GetInnerTextXML(document, "PublicationDate"), out dtPublicTemp);
                this.PublicationDate = dtPublicTemp;

                UpdateDescriptiones = GetInnerTextXML(document, "UpdateDescriptiones");
                RootDirTemp = GetInnerTextXML(document, "RootDirTemp");
                RootDirUpdate = GetInnerTextXML(document, "RootDirUpdate");
                RootDirBackUp = GetInnerTextXML(document, "RootDirBackUp");
                LogFile = GetInnerTextXML(document, "LogFile");

                bool useBakcUpFiles = false;
                bool.TryParse(GetInnerTextXML(document, "UseBakcUpFiles"), out useBakcUpFiles);
                UseBakcUpFiles = useBakcUpFiles;

                bool showWindowProcessInstall = false;
                bool.TryParse(GetInnerTextXML(document, "ShowWindowProcessInstall"), out showWindowProcessInstall);
                ShowWindowProcessInstall = showWindowProcessInstall;

                var runApplicationes = GetNodeXML(document, "RunApplicationes");
                if (runApplicationes != null)
                    foreach (XmlNode nod in runApplicationes.ChildNodes)
                    {
                        bool isService = false;
                        Boolean.TryParse(GetAtributeTextXML(nod, "IsService"), out isService);
                        this.RunApplicationes.Add(new RunApplication(GetAtributeTextXML(nod, "LocalPath"), GetAtributeTextXML(nod, "Argumentes"), GetAtributeTextXML(nod, "NameService"), isService));
                    }

                var installFiles = GetNodeXML(document, "InstallFiles");
                foreach (XmlNode nod in installFiles.ChildNodes)
                    this.InstallFiles.Add(new FileInstall(GetAtributeTextXML(nod, "SourceFile"), GetAtributeTextXML(nod, "DestFile")));

                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Ошибка чтения инструкции установки файлов: " + ex.Message, "UpdateInstaller", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

            }

            return false;
        }
        /// <summary>
        /// Начать установку
        /// </summary>
        public Task BeginInstall()
        {
            this.IsRunInstalling = true;

            Task task = Task.Factory.StartNew(() =>
             {
                 try
                 {
                     System.Threading.Thread.Sleep(3000);

                     //Останвока запущенных служб и приложений
                     foreach (RunApplication runApp in this.RunApplicationes)
                     {
                         if (runApp.IsService == false)
                         {
                             try
                             {
                                 if (ApplicationUtilites.ExistRunApplication(runApp.LocalPath))
                                 {
                                     ApplicationUtilites.StopApplication(runApp.LocalPath);
                                     runApp.IsRun = true;
                                     this.OnChangingPhaseOperationEvent(this, new ChangingPhaseOperationEventArgs("Остановка приложениея: " + System.IO.Path.GetFileName(runApp.LocalPath)));
                                 }
                             }
                             catch (Exception ex)
                             {
                                 throw new Exception("Не удалось остановить приложение: " + System.IO.Path.GetFileName(runApp.LocalPath), ex);
                             }
                         }
                         else
                         {
                             try
                             {
                                 if (ServiceUtilites.ServiceExistence(runApp.NameService) && ServiceUtilites.ServiceState(runApp.NameService) == System.ServiceProcess.ServiceControllerStatus.Running)
                                 {
                                     ServiceUtilites.StopService(runApp.NameService);
                                     System.Threading.Thread.Sleep(5000);
                                     runApp.IsRun = true;
                                     this.OnChangingPhaseOperationEvent(this, new ChangingPhaseOperationEventArgs("Остановка службы: " + runApp.NameService));
                                 }
                             }
                             catch (Exception ex)
                             {
                                 throw new Exception("Не удалось остановить службу: " + runApp.NameService, ex);
                             }
                         }
                     }

                     //Вызов событиея "Старт установки файлов"
                     this.OnBeginInstallEvent(this, new BeginInstallEventArgs(this.InstallFiles.Count));

                     if (UseBakcUpFiles)
                         new BackUManager(RootDirBackUp, RootDirUpdate).BackUpFiles(CurrentVersion.ToString(), this.InstallFiles);

                     //Устанвока файлов
                     foreach (FileInstall file in this.InstallFiles)
                     {
                         this.BeginInstallFileEvent(this, new InstallFileEventArgs(file));

                         file.InstalFile();

                         this.OnInstalledFileEvent(this, new InstallFileEventArgs(file));
                     }

                     throw  new Exception();

                     //Удаление файлов источников
                     foreach (FileInstall file in this.InstallFiles)
                     {
                         try
                         {
                             file.DeleteFileSource();
                         }
                         catch
                         {
                         }
                     }
                 }
                 catch (Exception ex)
                 {
                     if (UseBakcUpFiles)
                         new BackUManager(RootDirBackUp, RootDirUpdate).RecoveryFiles(CurrentVersion.ToString());

                     this.IsErrors = true;
                     this.OnErrorEvent(this, new ErrorEventArgs(ex));
                 }

                 this.IsRunInstalling = false;
                 this.OnEndInstallEvent(this, EventArgs.Empty);
             });

            return task;
        }
        /// <summary>
        /// Запустить ранее остановленные приложения
        /// </summary>
        public void RunApplicationStopped()
        {
            //Запуск остановленных приложений
            foreach (RunApplication runApp in this.RunApplicationes)
            {
                if (runApp.IsRun)
                {
                    if (runApp.IsService == false)
                    {
                        try
                        {
                            ApplicationUtilites.StartApplication(runApp.LocalPath, runApp.Argumentes);
                            this.OnChangingPhaseOperationEvent(this, new ChangingPhaseOperationEventArgs("Запуск приложениея: " + System.IO.Path.GetFileName(runApp.LocalPath)));
                        }
                        catch (Exception ex)
                        {
                            this.OnErrorEvent(this, new ErrorEventArgs(ex));
                        }
                    }
                    else
                    {
                        try
                        {
                            if (ServiceUtilites.ServiceExistence(runApp.NameService) && ServiceUtilites.ServiceState(runApp.NameService) == System.ServiceProcess.ServiceControllerStatus.Stopped)
                            {
                                ServiceUtilites.StartService(runApp.NameService);
                                this.OnChangingPhaseOperationEvent(this, new ChangingPhaseOperationEventArgs("Запуск службы: " + System.IO.Path.GetFileName(runApp.LocalPath)));
                            }
                        }
                        catch (Exception ex)
                        {
                            this.OnErrorEvent(this, new ErrorEventArgs(ex));
                        }
                    }
                }
            }
        }

        #region Private methodes

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

        #endregion
    }
}
