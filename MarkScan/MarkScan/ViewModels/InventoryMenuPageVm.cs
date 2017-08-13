using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarkScan.Pages;

namespace MarkScan.ViewModels
{
    internal class InventoryMenuPageVm
    {
        internal void GoToMainMenuPage()
        {
            App._mainWindowsVm._generalFrame.Navigate(new Uri(@"pack://application:,,,/" + AppSettings.NameAssembly + ";component/Pages/MainMenuPage.xaml", UriKind.Absolute));
        }

        internal void GoToMarkScan()
        {
            App._mainWindowsVm._generalFrame.Navigate(new Uri(@"pack://application:,,,/" + AppSettings.NameAssembly + ";component/Pages/MarkScanPage.xaml", UriKind.Absolute));
            App._mainWindowsVm._generalFrame.ContentRendered += _generalFrame_ContentRendered;
        }

        private void _generalFrame_ContentRendered(object sender, EventArgs e)
        {
            var page = App._mainWindowsVm._generalFrame.Content as MarkScanPage;
            page.SetOpearation(ETypeOperations.Inventory);

            App._mainWindowsVm._generalFrame.ContentRendered -= _generalFrame_ContentRendered;
        }
    }
}
