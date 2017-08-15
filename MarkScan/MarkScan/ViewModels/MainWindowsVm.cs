using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkScan.ViewModels
{
    internal class MainWindowsVm
    {
        internal System.Windows.Controls.Frame _generalFrame;
        internal MainWindow _MainWindow;

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
            _MainWindow.verLb.Content = AppSettings.VerAssembly;
            _MainWindow.loginLb.Content = AppSettings.settings.Login;
        }

    }
}
