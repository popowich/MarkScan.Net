using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using MarkScan.RetailEquipment;
using MarkScan.ViewModels;
using Application = System.Windows.Application;

namespace MarkScan
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string AppGuid = "47E770B3-E0B6-4BF9-8823-14E87D9BD5C0";

        private Mutex _mutex;

        internal static ViewModels.MainWindowsVm _mainWindowsVm = new MainWindowsVm();

        protected override void OnStartup(StartupEventArgs e)
        {
            _mutex = new Mutex(false, AppGuid);

            if (!_mutex.WaitOne(0, false))
            {
                BringWindowToFront();

                Shutdown();
                return;
            }

            AppSettings._settings.LoadSettings();

            if (HidSacnerManager.IsReady)
                HidSacnerManager._hidScaner.StartRead();

            RetailEquipment.HidSacnerManager._hidScaner.ReadDataEvent += hidScaner_ReadDataEvent;

            Network.CvcOpenApi.GetClientApi().SetTokenAuth(AppSettings._settings.Login, AppSettings._settings.Pass);

            Data.RemainingsManager.GetManager().UpdateRemainings();

            Updater.UpdateService.GetService().SatrtChekUpate();

            base.OnStartup(e);
        }

        private void hidScaner_ReadDataEvent(object sender, RetailEquipment.HIDBarcodeReaderEventArgs e)
        {
            App._mainWindowsVm._mainWindow.Dispatcher.Invoke((Action)delegate
            {
                bool res = _mainWindowsVm._generalFrame == null || _mainWindowsVm._generalFrame.Content is Pages.MarkScanPage == false;
                if (res)
                {
                    _mainWindowsVm.SetWindowShow();
                    _mainWindowsVm.ShowBalloonTip("Откройте страницу ввода", "Ввод отклонен", true);
                }
            });
        }

        protected override void OnExit(ExitEventArgs e)
        {
            HidSacnerManager._hidScaner.Dispose();

            Updater.UpdateService.GetService().Dispose();

            AppSettings._settings.SaveSetting();

            _mutex.Dispose();

            base.OnExit(e);
        }

        #region user32.dll import

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, ShowWindowEnum flags);

        private enum ShowWindowEnum
        {
            Hide = 0,
            ShowNormal = 1,
            ShowMinimized = 2,
            ShowMaximized = 3,
            Maximize = 3,
            ShowNormalNoActivate = 4,
            Show = 5,
            Minimize = 6,
            ShowMinNoActivate = 7,
            ShowNoActivate = 8,
            Restore = 9,
            ShowDefault = 10,
            ForceMinimized = 11
        };

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hwnd);

        public static void BringWindowToFront()
        {
            //get the process
            //            var bProcess = System.Diagnostics.Process.GetProcessesByName("Egaiska").FirstOrDefault();

            foreach (var proc in System.Diagnostics.Process.GetProcessesByName("MarkScan"))
            {
                //get the (int) hWnd of the process
                var hwnd = proc.MainWindowHandle;

                //check if its nothing
                if (hwnd.ToInt32() != 0)
                {
                    //if the handle is other than 0, then set the active window
                    SwitchToThisWindow(hwnd, true);
                    SetForegroundWindow(hwnd);
                }
                else
                {
                    //we can assume that it is fully hidden or minimized, so lets show it!
                    ShowWindow(proc.Handle, ShowWindowEnum.Restore);
                    SetForegroundWindow(proc.MainWindowHandle);
                    SwitchToThisWindow(proc.MainWindowHandle, true);
                }
            }

            //check if the process is nothing or not.
            //            if (bProcess == null) return;
        }

        #endregion
    }
}
