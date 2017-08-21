using OnlineUpdate;
using System;
using System.ComponentModel;
using System.IO;
using MarkScan.ViewModels;

namespace MarkScan.Updater
{
    internal class UpdateService
    {
        private static UpdateService Instance;

        private  UpdateManager upManager;

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
            opUpdate.UseBakcUpFiles = true;

            UpdateManager.InitUpdateManager(opUpdate);

            upManager = new UpdateManager();
            upManager.ErrorEvent += UpManager_ErrorEvent;
            upManager.EndDownloadFilesEvent += UpManager_EndDownloadFilesEvent;
            upManager.EndCheckUpdateEvent += UpManager_EndCheckUpdateEvent;
        }

        internal void SatrtChekUpate()
        {
            upManager.BeginCheckUpdatesAsync();
        }

        internal void SatrtUpate()
        {
            upManager.BeginUpdate();
        }

        internal  void Dispose()
        {
            if (upManager != null)
                upManager.Dispose();

        }

        #region Internal handler

        private void UpManager_EndCheckUpdateEvent(object sender, OnlineUpdate.UpdaterEventArgs.EndChekUpdateEventArgs e)
        {
            App._mainWindowsVm._generalFrame.Navigate(
                new Pages.AppUpdateDescriptionPage(new AppUpdateDescriptopnVm(e.Description)));
        }

        private  void UpManager_EndDownloadFilesEvent(object sender, OnlineUpdate.UpdaterEventArgs.EndLoadUpdateFilesEventArgs e)
        {
            //if (e.Description != null && e.Description.AllowUpdate)
            //{
            //    App._mainWindowsVm._MainWindow.Dispatcher.Invoke((Action)delegate
            //    {
            //        UpdateDescriptionForm formUpdate = new UpdateDescriptionForm(e.Description, $"\"{App._mainWindowsVm._MainWindow.Title}\" - обновление", Properties.Resources.CVC);
            //        if (formUpdate.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            //            e.Cancel = true;
            //    });
            //}
        }

        private  void UpManager_ErrorEvent(object sender, ErrorEventArgs e)
        {
            AppSettings.HandlerException(new Exception("OnlineUpdate error", e.GetException()));
        }

        #endregion
    }
}
