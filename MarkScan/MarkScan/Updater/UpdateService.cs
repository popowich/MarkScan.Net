using OnlineUpdate;
using System;
using System.ComponentModel;
using System.IO;

namespace MarkScan.Updater
{
    internal class UpdateService
    {
        private static UpdateService Instance;

        private  UpdateManager upManager;

        private  System.Timers.Timer _timerProcessUpdate;

        internal static UpdateService GetService()
        {
            return Instance ?? (Instance = new UpdateService());
        }

        internal void InitUpdateService()
        {
            string appLocation = System.Reflection.Assembly.GetEntryAssembly().Location;

            UpdateOptiones opUpdate = new UpdateOptiones("http://localhost/MainlUpdate", AppSettings.UpdateDir, AppSettings.CurrDir, appLocation, true);
            opUpdate.PatchFileInstallog = AppSettings.CurrDir + "\\" + UpdateOptiones.nameFileUpdateLog;
            opUpdate.UseFileCompression = false;

            UpdateManager.InitUpdateManager(opUpdate);

            upManager = new UpdateManager();
            upManager.ErrorEvent += UpManager_ErrorEvent;
            upManager.EndDownloadFilesEvent += UpManager_EndDownloadFilesEvent;

            initTimerProcesssUpdate();
        }

        internal  void Dispose()
        {
            if (upManager != null)
                upManager.Dispose();

            if (_timerProcessUpdate != null)
                _timerProcessUpdate.Dispose();
        }

        private  void initTimerProcesssUpdate()
        {
            _timerProcessUpdate = new System.Timers.Timer(5000);

            _timerProcessUpdate.Elapsed += onTimedEvent;
            _timerProcessUpdate.AutoReset = true;
            _timerProcessUpdate.Start();
        }

        private  void onTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            _timerProcessUpdate.Enabled = false;

            upManager.BeginUpdate();
        }

        #region Internal handler

        private  void UpManager_EndDownloadFilesEvent(object sender, OnlineUpdate.UpdaterEventArgs.EndLoadUpdateFilesEventArgs e)
        {
            if (e.Description != null && e.Description.AllowUpdate)
            {
                App._mainWindowsVm._MainWindow.Dispatcher.Invoke((Action)delegate
                {
                    UpdateDescriptionForm formUpdate = new UpdateDescriptionForm(e.Description, $"\"{App._mainWindowsVm._MainWindow.Title}\" - обновление", Properties.Resources.CVC);
                    if (formUpdate.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                        e.Cancel = true;
                });
            }

            _timerProcessUpdate.Interval = 1800000;
            _timerProcessUpdate.Enabled = true;
        }

        private  void UpManager_ErrorEvent(object sender, ErrorEventArgs e)
        {
            AppSettings.HandlerException(new Exception("OnlineUpdate error", e.GetException()));
        }

        #endregion
    }
}
