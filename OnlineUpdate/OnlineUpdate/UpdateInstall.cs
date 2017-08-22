using OnlineUpdate.FileInstaller;
using System;
using System.Collections.Generic;
using System.IO;

namespace OnlineUpdate
{
    public class UpdateInstall : IUpdateInstall
    {
        private List<FileUpdate> _updateFiles = new List<FileUpdate>();

        private IUpdateCheck _updateInfo;

        #region

        /// <summary>
        /// Обработчик начала установки файлов
        /// </summary>
        public event EventHandler<BeginPreparingInstallationEventArgs> BeginPreparingInstallationEvent;
        /// <summary>
        /// Обработчик окончания установки файлов
        /// </summary>
        public event EventHandler<EndPreparingInstallationEventArgs> EndPreparingInstallationEvent;

        protected void OnBeginPreparingInstallationEvent(Object _sender, BeginPreparingInstallationEventArgs _e)
        {
            var eventHandler = BeginPreparingInstallationEvent;
            if (eventHandler != null)
                eventHandler(_sender, _e);
        }

        protected void OnEndPreparingInstallationEvent(Object _sender, EndPreparingInstallationEventArgs _e)
        {
            var eventHandler = EndPreparingInstallationEvent;
            if (eventHandler != null)
                eventHandler(_sender, _e);
        }

        #endregion

        public UpdateInstall(IUpdateCheck updateInfo)
        {
            _updateInfo = updateInfo;
            _updateFiles = updateInfo.UpdateFiles;
        }

        public void Dispose()
        {
            if (this.BeginPreparingInstallationEvent != null)
                foreach (EventHandler<BeginPreparingInstallationEventArgs> d in this.BeginPreparingInstallationEvent.GetInvocationList())
                    this.BeginPreparingInstallationEvent -= d;

            if (this.EndPreparingInstallationEvent != null)
                foreach (EventHandler<EndPreparingInstallationEventArgs> d in this.EndPreparingInstallationEvent.GetInvocationList())
                    this.EndPreparingInstallationEvent -= d;
        }

        public void InstallFiles()
        {
            try
            {
                //Вызов событиея "Старт установки файлов"
                this.OnBeginPreparingInstallationEvent(this, new BeginPreparingInstallationEventArgs(this._updateFiles.Count));

                //Проверка возможности установки
                foreach (FileUpdate file in this._updateFiles)
                {
                    if (file.IsFit == false)
                    {
                        throw new Exception("File is not suitable for installation: " + file.RelativeUrlFile);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing the update files", ex);
            }

            saveInstalInstructione(UpdateManager.Options.DirTempFiles + "\\" + UpdateOptiones.fileInstallInstructiones);

            this.OnEndPreparingInstallationEvent(this, new EndPreparingInstallationEventArgs(true));

            runAppInstall();
        }

        protected virtual void saveInstalInstructione(string _file_name)
        {
            try
            {
                System.Xml.XmlDocument document = new System.Xml.XmlDocument();
                document.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><head></head>");

                System.Xml.XmlNode element = null;

                element = document.CreateElement("CurrentVersion");
                element.InnerText = _updateInfo.CurrentVersion.ToString();
                document.DocumentElement.AppendChild(element);

                if (_updateInfo.UpgradeToVersion != null)
                {
                    element = document.CreateElement("UpgradeToVersion");
                    element.InnerText = _updateInfo.UpgradeToVersion.ToString();
                    document.DocumentElement.AppendChild(element);
                }
                if (_updateInfo.PublicationDate != null)
                {
                    element = document.CreateElement("PublicationDate");
                    element.InnerText = _updateInfo.PublicationDate.ToString();
                    document.DocumentElement.AppendChild(element);
                }
                if (!string.IsNullOrEmpty(_updateInfo.UpdateDescriptiones))
                {
                    element = document.CreateElement("UpdateDescriptiones");
                    element.InnerText = _updateInfo.UpdateDescriptiones;
                    document.DocumentElement.AppendChild(element);
                }

                element = document.CreateElement("UseBakcUpFiles");
                element.InnerText = UpdateManager.Options.UseBakcUpFiles.ToString();
                document.DocumentElement.AppendChild(element);

                element = document.CreateElement("RootDirTemp");
                element.InnerText = UpdateManager.Options.DirTempFiles;
                document.DocumentElement.AppendChild(element);

                element = document.CreateElement("RootDirBackUp");
                element.InnerText = UpdateManager.Options.RootDirBackUp;
                document.DocumentElement.AppendChild(element);

                element = document.CreateElement("RootDirUpdate");
                element.InnerText = UpdateManager.Options.RootDirUpdate;
                document.DocumentElement.AppendChild(element);

                element = document.CreateElement("LogFile");
                element.InnerText = UpdateManager.Options.PatchFileInstallog;
                document.DocumentElement.AppendChild(element);

                            
                element = document.CreateElement("ShowWindowProcessInstall");
                element.InnerText = UpdateManager.Options.ShowWindowProcessInstall.ToString();
                document.DocumentElement.AppendChild(element);

                element = document.CreateElement("RunApplicationes");
                document.DocumentElement.AppendChild(element);

                if (UpdateManager.Options.RunApplicationes != null)
                    foreach (RunApplication file in UpdateManager.Options.RunApplicationes)
                    {
                        System.Xml.XmlNode elementCild = document.CreateElement("App");
                        elementCild.Attributes.Append(document.CreateAttribute("LocalPath")).InnerText = file.LocalPath;
                        elementCild.Attributes.Append(document.CreateAttribute("NameService")).InnerText = file.NameService;
                        elementCild.Attributes.Append(document.CreateAttribute("IsService")).InnerText = file.IsService.ToString();
                        elementCild.Attributes.Append(document.CreateAttribute("Argumentes")).InnerText = file.Argumentes;
                        element.AppendChild(elementCild);
                    }

                element = document.CreateElement("InstallFiles");
                document.DocumentElement.AppendChild(element);

                foreach (FileUpdate file in _updateFiles)
                {
                    System.Xml.XmlNode elementCild = document.CreateElement("File");
                    elementCild.Attributes.Append(document.CreateAttribute("SourceFile")).InnerText = file.PatchFileTemp + "\\" + file.NameFileInstall;
                    elementCild.Attributes.Append(document.CreateAttribute("DestFile")).InnerText = file.PathFileInstall + "\\" + file.NameFileInstall;
                    element.AppendChild(elementCild);
                }

                //Сохраняем xml файл
                document.Save(_file_name);
            }
            catch (Exception ex)
            {
                throw new Exception("Error writing file installer instructions", ex);
            }
        }

        protected void runAppInstall()
        {
            try
            {
                if(!File.Exists(UpdateManager.Options.DirTempFiles + "\\" + UpdateOptiones.appInstallFiles))
                {
                    File.Copy(Directory.GetCurrentDirectory() + "\\" + UpdateOptiones.appInstallFiles, UpdateManager.Options.DirTempFiles + "\\" + UpdateOptiones.appInstallFiles);
                }

                //Необходимость запроса прав администратора
                bool allow_admin_role = false;
                //Проверяем на необходимость административных прав
                string system_dir = Environment.GetFolderPath(Environment.SpecialFolder.System);
                //Получаем текущие права
                System.Security.Principal.WindowsIdentity id = System.Security.Principal.WindowsIdentity.GetCurrent();
                System.Security.Principal.WindowsPrincipal p = new System.Security.Principal.WindowsPrincipal(id);
                if (!p.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator) && System.IO.Path.GetPathRoot(system_dir) == System.IO.Path.GetPathRoot(UpdateManager.Options.RootDirUpdate))
                    allow_admin_role = true;

                //Запуск процесса инсталяции
                System.Diagnostics.ProcessStartInfo processInfo = new System.Diagnostics.ProcessStartInfo();
                processInfo.FileName = UpdateManager.Options.DirTempFiles + "\\" + UpdateOptiones.appInstallFiles;

                if (allow_admin_role)
                {
                    processInfo.UseShellExecute = true;
                    processInfo.Verb = "runas";
                }

                //processInfo.UseShellExecute = false;
                //processInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                //processInfo.CreateNoWindow = true;

                processInfo.Arguments = UpdateOptiones.fileInstallInstructiones;
                System.Diagnostics.Process prc_instal = new System.Diagnostics.Process();
                prc_instal.StartInfo = processInfo;
                prc_instal.Start();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to start a special installation", ex);
            }

        }
    }
}
