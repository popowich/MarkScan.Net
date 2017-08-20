using OnlineUpdate.UpdaterEventArgs;
using OnlineUpdate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace OnlineUpdate
{
    public class UpdateCheck : IUpdateCheck
    {
        private IDownloadFile _downloader;

        private CancellationTokenSource _cancaleTokenSource;

        /// <summary>
        /// Обновление разрешено
        /// </summary>
        public bool AllowUpdate { get; private set; }
        /// <summary>
        /// Обновление будет выполено до версии
        /// </summary>
        public Version UpgradeToVersion { get; set; }
        /// <summary>
        /// Дата публикации обновления
        /// </summary>
        public DateTime PublicationDate { get; set; }
        /// <summary>
        /// Текстовое описание обноваления
        /// </summary>
        public string UpdateDescriptiones { get; set; }
        /// <summary>
        /// Количество файлов для обновления
        /// </summary>
        public int CountUpdateFiles { get { return UpdateFiles.Count; } }
        /// <summary>
        /// список файлов для обновления
        /// </summary>
        public List<FileUpdate> UpdateFiles { get; private set; } = new List<FileUpdate>();

        #region Events

        public event EventHandler<BeginCheckUpdateEventArgs> BeginCheckUpdateEvent;

        public event EventHandler EndCheckCurrentFileEvent;

        public event EventHandler<EndChekUpdateEventArgs> EndCheckUpdateEvent;


        public void OnBeginCheckUpdateEvent(Object _sender, BeginCheckUpdateEventArgs _e)
        {
            var eventHandler = BeginCheckUpdateEvent;
            if (eventHandler != null)
                eventHandler(_sender, _e);
        }

        public void OnEndCheckCurrentFileEvent(Object _sender, EventArgs _e)
        {
            var eventHandler = EndCheckCurrentFileEvent;
            if (eventHandler != null)
                eventHandler(_sender, _e);
        }

        public void OnEndCheckUpdateEvent(Object _sender, EndChekUpdateEventArgs _e)
        {
            var eventHandler = EndCheckUpdateEvent;
            if (eventHandler != null)
                eventHandler(_sender, _e);
        }

        #endregion

        public UpdateCheck(IDownloadFile downloader)
        {
            _cancaleTokenSource = new CancellationTokenSource();
            _downloader = downloader;
        }

        public void Dispose()
        {
            if (this.BeginCheckUpdateEvent != null)
                foreach (EventHandler<BeginCheckUpdateEventArgs> d in this.BeginCheckUpdateEvent.GetInvocationList())
                    this.BeginCheckUpdateEvent -= d;

            if (this.EndCheckCurrentFileEvent != null)
                foreach (EventHandler d in this.EndCheckCurrentFileEvent.GetInvocationList())
                    this.EndCheckCurrentFileEvent -= d;

            if (this.EndCheckUpdateEvent != null)
                foreach (EventHandler<EndChekUpdateEventArgs> d in this.EndCheckUpdateEvent.GetInvocationList())
                    this.EndCheckUpdateEvent -= d;
        }

        public UpdateDescription CheckUpdates()
        {
            _cancaleTokenSource = new CancellationTokenSource();
            CancellationToken cancaleToken = _cancaleTokenSource.Token;

            try
            {
                return checkUpdates(cancaleToken);
            }
            catch (OperationCanceledException)
            {

            }
            catch (Exception ex)
            {
                var desc = new UpdateDescription() { AllowUpdate = AllowUpdate, PublicationDate = PublicationDate, UpdateDescriptiones = UpdateDescriptiones, UpgradeToVersion = UpgradeToVersion, CountFiles = this.CountUpdateFiles };

                this.OnEndCheckUpdateEvent(this, new EndChekUpdateEventArgs() { Description = desc });

                throw ex;
            }

            return null;
        }

        public void BeginCheckUpdates()
        {
            _cancaleTokenSource = new CancellationTokenSource();
            CancellationToken cancaleToken = _cancaleTokenSource.Token;

            try
            {
                Task.Factory.StartNew(() =>
                {
                    checkUpdates(cancaleToken);

                }, cancaleToken);
            }
            catch (OperationCanceledException)
            {

            }
            catch (Exception ex)
            {
                var desc = new UpdateDescription() { AllowUpdate = AllowUpdate, PublicationDate = PublicationDate, UpdateDescriptiones = UpdateDescriptiones, UpgradeToVersion = UpgradeToVersion, CountFiles = this.CountUpdateFiles };

                this.OnEndCheckUpdateEvent(this, new EndChekUpdateEventArgs() { Description = desc });

                throw ex;
            }
        }

        public void StopCheckUpdates()
        {
            if (_cancaleTokenSource != null)
                _cancaleTokenSource.Cancel();
        }

        protected UpdateDescription checkUpdates(CancellationToken cancaleToken)
        {
            AllowUpdate = false;
            this.UpdateFiles.Clear();

            this.UpgradeToVersion = null;
            this.PublicationDate = DateTime.MinValue;

            if (!Directory.Exists(UpdateManager.Options.DirTempFiles))
                throw new Exception("Not Detected catalog subsystem updates");

            //Загрузка файла описания обновления
            downloadDescriptioneFile();

            //Читаем и разбираем файл описания обновления
            List<FileUpdate> files = parseDescriptioneFile();

            this.OnBeginCheckUpdateEvent(this, new OnlineUpdate.UpdaterEventArgs.BeginCheckUpdateEventArgs(files.Count));

            //Проверка доступности обновления
            bool updateAval = false;

            if (files.Count > 0)
            {
                if (this.UpgradeToVersion != null)
                    updateAval = this.UpgradeToVersion.CompareTo(UpdateManager.Options.CurrentVersionUpdateApp) > 0;
                else
                    updateAval = true;
            }

            //Проверяем спсиок файлов на необходимость обновления
            if (updateAval == true)
            {
                foreach (FileUpdate file in files)
                {
                    if (cancaleToken.IsCancellationRequested)
                        cancaleToken.ThrowIfCancellationRequested();

                    if (file.GetNeedToUpdateFile())
                        this.UpdateFiles.Add(file);

                    if (cancaleToken.IsCancellationRequested)
                        cancaleToken.ThrowIfCancellationRequested();

                    this.OnEndCheckCurrentFileEvent(this, EventArgs.Empty);
                }
            }

            AllowUpdate = this.CountUpdateFiles > 0 && updateAval;

            var desc = new UpdateDescription() { AllowUpdate = AllowUpdate, PublicationDate = PublicationDate, UpdateDescriptiones = UpdateDescriptiones, UpgradeToVersion = UpgradeToVersion, CountFiles = this.CountUpdateFiles };

            //Вызвать обработчик завршения проверки наличия обновления
            this.OnEndCheckUpdateEvent(this, new EndChekUpdateEventArgs() { Description = desc });

            return desc;
        }

        protected bool downloadDescriptioneFile()
        {
            try
            {
                if (File.Exists(UpdateManager.Options.PatchFileDescriptione + UpdateManager.Options.FileExtension))
                    File.Delete(UpdateManager.Options.PatchFileDescriptione + UpdateManager.Options.FileExtension);

                if (File.Exists(UpdateManager.Options.PatchFileDescriptione))
                    File.Delete(UpdateManager.Options.PatchFileDescriptione);

                bool res = _downloader.TryDownloadFile(UpdateManager.Options.UrlFileDescriptione, UpdateManager.Options.PatchFileDescriptione + UpdateManager.Options.FileExtension);

                if (res && UpdateManager.Options.UseFileCompression)
                {
                    FileUtilities.DecompressBz2(UpdateManager.Options.PatchFileDescriptione + UpdateManager.Options.FileExtension, UpdateManager.Options.PatchFileDescriptione);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading description file updates", ex);
            }

        }

        protected virtual List<FileUpdate> parseDescriptioneFile()
        {
            List<FileUpdate> files = new List<FileUpdate>();
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(UpdateManager.Options.PatchFileDescriptione);

                XmlNode element = XMLUtilites.GetNodeXML(document, "UpgradeToVersion");

                UpgradeToVersion = Version.Parse(element.InnerText);

                element = XMLUtilites.GetNodeXML(document, "PublicationDate");

                DateTime dTemp;
                DateTime.TryParse(element.InnerText, out dTemp);
                PublicationDate = dTemp;

                element = XMLUtilites.GetNodeXML(document, "UpdateDescriptiones");
                UpdateDescriptiones = element.InnerText;

                XmlNode elementFiles = XMLUtilites.GetNodeXML(document, "UpdateFiles");
                if (elementFiles != null)
                {
                    foreach (XmlNode nod in elementFiles.ChildNodes)
                    {
                        string pathInstall = XMLUtilites.GetAtributeTextXML(nod, "PathInstall");
                        string pathDownload = XMLUtilites.GetAtributeTextXML(nod, "PathDownload");
                        string newMD5 = XMLUtilites.GetAtributeTextXML(nod, "MD5");

                        string localLink = UpdateManager.Options.UrlHostUpdate + pathDownload.Replace('\\', '/');

                        FileUpdate newFile = new FileUpdate(pathInstall, pathDownload, localLink, newMD5, null);
                        files.Add(newFile);
                    }
                }

                return files;
            }
            catch (Exception ex)
            {
                throw new Exception("Error reading and parsing the definition file updates", ex);
            }
        }
    }
}
