using OnlineUpdate;
using System;
using System.ComponentModel;
using System.IO;
using MarkScan.ViewModels;

namespace MarkScan.Updater
{
    /// <summary>
    /// Сервис обновлений
    /// </summary>
    internal class UpdateService
    {
        private static UpdateService Instance;

        private UpdateManager upManager;

        internal static UpdateService GetService()
        {
            return Instance ?? (Instance = new UpdateService());
        }

        internal UpdateService()
        {
            string appLocation = System.Reflection.Assembly.GetEntryAssembly().Location;

            UpdateOptiones opUpdate = new UpdateOptiones("http://localhost/MainlUpdate", AppSettings.UpdateDir, AppSettings.CurrDir, appLocation, true);
            opUpdate.PatchFileInstallog = AppSettings.CurrDir + "\\" + UpdateOptiones.nameFileUpdateLog;
            opUpdate.RootDirBackUp = AppSettings.UpdateDir + "\\BackUp";
            opUpdate.UseFileCompression = false;
            opUpdate.UseBakcUpFiles = true;

            opUpdate.RunApplicationes.Clear();
            opUpdate.RunApplicationes.Add(new RunApplication(appLocation, "-postUpdate", null, false));

            UpdateManager.InitUpdateManager(opUpdate);

            upManager = new UpdateManager();
            upManager.ErrorEvent += UpManager_ErrorEvent;
            upManager.EndDownloadFilesEvent += UpManager_EndDownloadFilesEvent;
            upManager.EndCheckUpdateEvent += UpManager_EndCheckUpdateEvent;

            updateFileInstall();
        }
        /// <summary>
        /// Проверить наличие обновлений
        /// </summary>
        internal void SatrtChekUpate()
        {
            upManager.BeginCheckUpdates();
        }
        /// <summary>
        /// Выполинть обновление
        /// </summary>
        internal void SatrtUpate()
        {
            upManager.BeginUpdate();
        }

        internal void Dispose()
        {
            if (upManager != null)
                upManager.Dispose();

        }

        private void updateFileInstall()
        {
            try
            {
                if (File.Exists(AppSettings.CurrDir + "\\" + UpdateOptiones.appInstallFiles))
                    File.Delete(AppSettings.CurrDir + "\\" + UpdateOptiones.appInstallFiles);

                File.WriteAllBytes(AppSettings.UpdateDir + "\\" + UpdateOptiones.appInstallFiles, Properties.Resources.UpdateInstaller);
            }
            catch (Exception ex)
            {
                AppSettings.HandlerException(new Exception("Error copy file installer", ex));
            }
        }

        #region Handlers events update

        private void UpManager_EndCheckUpdateEvent(object sender, OnlineUpdate.UpdaterEventArgs.EndChekUpdateEventArgs e)
        {
            if (e.Description == null || e.Description.AllowUpdate == false)
                return;

            App._mainWindowsVm._MainWindow.Dispatcher.Invoke((Action)delegate
            {
                App._mainWindowsVm._generalFrame.Navigate(
                    new Pages.AppUpdateDescriptionPage(new AppUpdateDescriptopnVm(e.Description)));
            });
        }

        private void UpManager_EndDownloadFilesEvent(object sender, OnlineUpdate.UpdaterEventArgs.EndLoadUpdateFilesEventArgs e)
        {

        }

        private void UpManager_ErrorEvent(object sender, ErrorEventArgs e)
        {
            AppSettings.HandlerException(new Exception("OnlineUpdate error", e.GetException()));
        }

        #endregion
    }
}
