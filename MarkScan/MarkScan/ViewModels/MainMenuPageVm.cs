using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkScan.ViewModels
{
    class MainMenuPageVm
    {
        internal void GoToInventoryMenuPage()
        {
            App._mainWindowsVm._generalFrame.Navigate(new Uri(@"pack://application:,,,/" + AppSettings.NameAssembly + ";component/Pages/InventoryMenuPage.xaml", UriKind.Absolute));
        }
    }
}
