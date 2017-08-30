using System;

namespace MarkScan.ViewModels
{
    internal class MainWindowsVm
    {
        internal System.Windows.Controls.Frame _generalFrame;
        internal MainWindow _mainWindow;

        internal void GoToAuthPage()
        {
            _generalFrame.Navigate(new Uri(@"pack://application:,,,/" + AppSettings.NameAssembly + ";component/Pages/AuthPage.xaml", UriKind.Absolute));
        }

        internal void GoToMainMenuPage()
        {
            _generalFrame.Navigate(new Uri(@"pack://application:,,,/" + AppSettings.NameAssembly + ";component/Pages/MainMenuPage.xaml", UriKind.Absolute));
        }

        internal void SetVersion()
        {
            _mainWindow.verLb.Content = AppSettings.VerAssembly;
            _mainWindow.loginLb.Content = AppSettings._settings.Login;
        }

        internal void ChekUpdate()
        {
            Updater.UpdateService.GetService().SatrtChekUpate();
        }

        internal void SetWindowShow()
        {
            if (!_mainWindow.IsVisible)
                _mainWindow.Show();

            _mainWindow.WindowState = System.Windows.WindowState.Normal;
            _mainWindow.Activate();
            _mainWindow.Focus();
        }

        internal void SetWindowHide()
        {
            _mainWindow.Hide();
        }

        internal void ShowBalloonTip(string title, string text, bool isWarning)
        {
            _mainWindow._notify_icon.ShowBalloonTip(1, title, text, isWarning == true ? System.Windows.Forms.ToolTipIcon.Warning : System.Windows.Forms.ToolTipIcon.Info);
        }
    }
}
