using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkScan.ViewModels
{
    class InventoryMenuPageVm
    {
        internal void GoToMainMenuPage()
        {
            App._mainWindowsVm._generalFrame.Navigate(new Uri(@"pack://application:,,,/" + AppSettings.NameAssembly + ";component/Pages/MainMenuPage.xaml", UriKind.Absolute));
        }

        internal void GoToMarkScan()
        {
            App._mainWindowsVm._generalFrame.Navigate(new Uri(@"pack://application:,,,/" + AppSettings.NameAssembly + ";component/Pages/MarkScanPage.xaml", UriKind.Absolute));
        }
    }
}
