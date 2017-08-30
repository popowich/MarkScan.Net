using OnlineUpdate;
using System;
using System.IO;
using MarkScan.ViewModels;

namespace MarkScan.Updater
{
    /// <summary>
    /// Сервис обновлений
    /// </summary>
    internal class UpdateService
    {
        private static UpdateService _instance;

        private UpdateManager _upManager;

        internal static UpdateService GetService()
        {
            return _instance ?? (_instance = new UpdateService());
        }

        internal UpdateService()
        {
            string appLocation = System.Reflection.Assembly.GetEntryAssembly().Location;

            UpdateOptiones opUpdate = new UpdateOptiones("http://188.227.19.100/WinCVC/MainUpdate", AppSettings.UpdateDir, AppSettings.CurrDir, appLocation, true);
            opUpdate.PatchFileInstallog = AppSettings.CurrDir + "\\" + UpdateOptiones.nameFileUpdateLog;
            opUpdate.RootDirBackUp = AppSettings.UpdateDir + "\\BackUp";
            opUpdate.UseFileCompression = false;
            opUpdate.UseBakcUpFiles = true;

            opUpdate.RunApplicationes.Clear();
            opUpdate.RunApplicationes.Add(new RunApplication(appLocation, "-postUpdate", null, false));

            UpdateManager.InitUpdateManager(opUpdate);

            _upManager = new UpdateManager();
            _upManager.ErrorEvent += UpManager_ErrorEvent;
            _upManager.EndCheckUpdateEvent += UpManager_EndCheckUpdateEvent;

            _updateFileInstall();
        }
        /// <summary>
        /// Проверить наличие обновлений
        /// </summary>
        internal void SatrtChekUpate()
        {
            _upManager.BeginCheckUpdates();
        }
        /// <summary>
        /// Выполинть обновление
        /// </summary>
        internal void SatrtUpate()
        {
            _upManager.BeginUpdate();
        }

        internal void Dispose()
        {
            if (_upManager != null)
                _upManager.Dispose();

        }

        private void _updateFileInstall()
        {
            try
            {
                if (File.Exists(AppSettings.UpdateDir + "\\" + UpdateOptiones.appInstallFiles))
                    File.Delete(AppSettings.UpdateDir + "\\" + UpdateOptiones.appInstallFiles);

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

            App._mainWindowsVm._mainWindow.Dispatcher.Invoke((Action)delegate
            {
                App._mainWindowsVm._generalFrame.Navigate(
                    new Pages.AppUpdateDescriptionPage(new AppUpdateDescriptopnVm(e.Description)));
            });
        }

        private void UpManager_ErrorEvent(object sender, ErrorEventArgs e)
        {
            AppSettings.HandlerException(new Exception("OnlineUpdate error", e.GetException()));
        }

        #endregion
    }
}
