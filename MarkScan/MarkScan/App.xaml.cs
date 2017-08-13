using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using MarkScan.ViewModels;

namespace MarkScan
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static ViewModels.MainWindowsVm _mainWindowsVm = new MainWindowsVm();

        protected override void OnStartup(StartupEventArgs e)
        {
            AppSettings.settings.LoadSettings();

            Network.CvcOpenApi.GetClientApi().SetTokenAuth(AppSettings.settings.Login, AppSettings.settings.Pass);

            base.OnStartup(e);
        }
    }
}
