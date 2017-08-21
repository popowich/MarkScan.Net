using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using OnlineUpdate.MultithreadedDownload;
using System.Threading;
using OnlineUpdate.UpdaterEventArgs;
using OnlineUpdate.FileInstaller;
using System.Reflection;

namespace OnlineUpdate
{
    /// <summary>
    /// Класс реализует механизм обновления
    /// </summary>
    public class UpdateManager : IDisposable
    {
        private IUpdateCheck _updateChecker;

        internal static IDownloadFile _fileDownloader;

        private IUpdateInstall _updateInstaller;

        private CancellationTokenSource _cancaleTokenSource = new CancellationTokenSource();

        public int CountUpdateFiles { get { return this._updateChecker.UpdateFiles.Count; } }

        public bool IsRunUpdate { get; private set; }

        public IUpdateCheck UpdateChecker { get { return _updateChecker; } }

        public static UpdateOptiones Options { get; private set; }

        #region Eventes

        /// <summary>
        /// Обрвботчик начала загрузки файлов обновления
        /// </summary>
        public event EventHandler<BeginDownloadFilesEventArgs> BeginDownloadFilesEvent;
        /// <summary>
        /// Обработчик окончания загрузки файлов обновления
        /// </summary>
        public event EventHandler<EndLoadUpdateFilesEventArgs> EndDownloadFilesEvent;
        /// <summary>
        /// Обработчик начала загрузки текущего файла
        /// </summary>
        public event EventHandler<StartDownloadEventArgs> BeginDownloadCurrentFileEvent;
        /// <summary>
        /// Обработчик окончания загрузки текущего файла
        /// </summary>
        public event EventHandler<LoadedFileUpdateEventArgs> EndDownloadCurrentFileEvent;
        /// <summary>
        /// Обработчик загрузки части файла
        /// </summary>
        public event EventHandler<SaveBuffEventArgs> ReadRangeCurrentFileEvent;
        /// <summary>
        /// Завершение проверки наличия обновления 
        /// </summary>
        public event EventHandler<EndChekUpdateEventArgs> EndCheckUpdateEvent;

        /// <summary>
        /// Обработчик исключительной ситуции 
        /// </summary>
        public event ErrorEventHandler ErrorEvent;


        public void OnBeginDownloadFilesEvent(Object _sender, BeginDownloadFilesEventArgs _e)
        {
            var eventHandler = BeginDownloadFilesEvent;
            if (eventHandler != null)
                eventHandler(_sender, _e);
        }

        public void OnEndDownloadFilesEvent(Object _sender, EndLoadUpdateFilesEventArgs _e)
        {
            var eventHandler = EndDownloadFilesEvent;
            if (eventHandler != null)
                eventHandler(_sender, _e);
        }

        public void OnBeginDownloadCurrentFileEvent(object _sender, StartDownloadEventArgs _e)
        {
            var eventHandler = BeginDownloadCurrentFileEvent;
            if (eventHandler != null)
                BeginDownloadCurrentFileEvent(_sender, _e);
        }

        public void OnEndDownloadCurrentFileEvent(Object _sender, LoadedFileUpdateEventArgs _e)
        {
            var eventHandler = EndDownloadCurrentFileEvent;
            if (eventHandler != null)
                eventHandler(_sender, _e);
        }

        public void OnReadRangeCurrentFileEvent(Object _sender, SaveBuffEventArgs _e)
        {
            var eventHandler = ReadRangeCurrentFileEvent;
            if (eventHandler != null)
                eventHandler(_sender, _e);
        }

        public void OnEndCheckUpdateEvent(Object _sender, EndChekUpdateEventArgs _e)
        {
            var eventHandler = EndCheckUpdateEvent;
            if (eventHandler != null)
                eventHandler(_sender, _e);
        }

        public void OnErrorEvent(Object _sender, ErrorEventArgs e)
        {
            var eventHandler = ErrorEvent;
            if (eventHandler != null)
                eventHandler(this, e);
        }

        #endregion

        public UpdateManager()
        {
            _fileDownloader = new Download.DownloadFileSync();
            _updateChecker = new UpdateCheck(_fileDownloader);
            _updateInstaller = new UpdateInstall(_updateChecker);
        }

        public static void InitUpdateManager(UpdateOptiones updateOption)
        {
            Options = updateOption;
        }

        public void Dispose()
        {
            _updateChecker.Dispose();
            _updateInstaller.Dispose();

            if (this.BeginDownloadFilesEvent != null)
                foreach (EventHandler<BeginDownloadFilesEventArgs> d in this.BeginDownloadFilesEvent.GetInvocationList())
                    this.BeginDownloadFilesEvent -= d;

            if (this.EndDownloadFilesEvent != null)
                foreach (EventHandler<EndLoadUpdateFilesEventArgs> d in this.EndDownloadFilesEvent.GetInvocationList())
                    this.EndDownloadFilesEvent -= d;

            if (this.BeginDownloadCurrentFileEvent != null)
                foreach (EventHandler<StartDownloadEventArgs> d in this.BeginDownloadCurrentFileEvent.GetInvocationList())
                    this.BeginDownloadCurrentFileEvent -= d;

            if (this.ReadRangeCurrentFileEvent != null)
                foreach (EventHandler<SaveBuffEventArgs> d in this.ReadRangeCurrentFileEvent.GetInvocationList())
                    this.ReadRangeCurrentFileEvent -= d;

            if (this.EndDownloadCurrentFileEvent != null)
                foreach (EventHandler<LoadedFileUpdateEventArgs> d in this.EndDownloadCurrentFileEvent.GetInvocationList())
                    this.EndDownloadCurrentFileEvent -= d;

            if (this.ErrorEvent != null)
                foreach (ErrorEventHandler d in this.ErrorEvent.GetInvocationList())
                    this.ErrorEvent -= d;
        }

        public UpdateDescription CheckUpdates()
        {
            try
            {
                return _updateChecker.CheckUpdates();
            }
            catch (Exception ex)
            {
                OnErrorEvent(_updateChecker, new ErrorEventArgs(ex));
                return null;
            }
        }

        public void BeginCheckUpdates()
        {
            var taskUpdate = Task.Factory.StartNew(() =>
            {
                try
                {
                    UpdateDescription res = CheckUpdates();
                    OnEndCheckUpdateEvent(this, new EndChekUpdateEventArgs() { Description = res });
                }
                catch (Exception ex)
                {
                    this.OnErrorEvent(this, new ErrorEventArgs(new Exception("Failure during an check update", ex)));
                }
            });
        }

        public void BeginUpdate()
        {
            CancellationToken cancaleToken = _cancaleTokenSource.Token;
            this.IsRunUpdate = true;

            var taskUpdate = Task.Factory.StartNew(() =>
            {
                if (IsRunUpdate)
                {
                    bool runInstall = false;
                    UpdateDescription checkResult = null;

                    try
                    {
                        //Проверка налаияи обновления
                        checkResult = _updateChecker.CheckUpdates();

                        if (checkResult != null && checkResult.AllowUpdate)
                        {
                            //Выполнить обработчик начала обновлений
                            this.OnBeginDownloadFilesEvent(this, new BeginDownloadFilesEventArgs(this.CountUpdateFiles));

                            if (!Directory.Exists(UpdateManager.Options.DirTempFiles))
                                throw new Exception("Not Detected catalog subsystem updates");

                            //Загружаем файлы во временную директорию
                            for (int i = 0; i < _updateChecker.UpdateFiles.Count && this.IsRunUpdate; i++)
                            {
                                if (cancaleToken.IsCancellationRequested)
                                    cancaleToken.ThrowIfCancellationRequested();

                                var file = _updateChecker.UpdateFiles[i];
                                OnBeginDownloadCurrentFileEvent(this, new StartDownloadEventArgs(file.NameFileInstall, 0, 0, 0));

                                //Загрузка файла обновления
                                file.DownloadFile();

                                if (cancaleToken.IsCancellationRequested)
                                    cancaleToken.ThrowIfCancellationRequested();

                                //Вызов события окончания загрузки очередного файла обновления
                                this.OnEndDownloadCurrentFileEvent(this, new LoadedFileUpdateEventArgs(file));
                            }

                            var ret = new EndLoadUpdateFilesEventArgs(checkResult, this._updateChecker.UpdateFiles, false);
                            this.OnEndDownloadFilesEvent(this, ret);

                            runInstall = !ret.Cancel;
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        this.IsRunUpdate = false;
                    }
                    catch (Exception ex)
                    {
                        IsRunUpdate = false;
                        runInstall = false;

                        this.OnErrorEvent(this, new ErrorEventArgs(new Exception("Failure during an update", ex)));
                    }

                    //Запускаем установку файлов обновления
                    if (this.IsRunUpdate == true
                        && runInstall == true
                        && cancaleToken.IsCancellationRequested == false)
                    {
                        _updateInstaller.InstallFiles();
                    }

                }
            }, cancaleToken);

        }

        public void StopUpdate()
        {
            this.IsRunUpdate = false;
            this._cancaleTokenSource.Cancel();
        }
    }
}
