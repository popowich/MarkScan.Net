using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarkScan.Models;
using MarkScan.Pages;

namespace MarkScan.ViewModels
{
    public class InventoryOpeationMenuPageVm: IOperationMenuVm
    {
        public void GoToMainMenuPage()
        {
            App._mainWindowsVm._generalFrame.Navigate(new Uri(@"pack://application:,,,/" + AppSettings.NameAssembly + ";component/Pages/MainMenuPage.xaml", UriKind.Absolute));
        }

        public void GoToMarkScanNew()
        {
            var model = new Models.MarkScanPageInventoryModel(false);

            if (model.ScanResults.Count > 0)
            {
                App._mainWindowsVm._generalFrame.Navigate( new Pages.QuaerePage(new ViewModels.QuaereInventiryMenuVm(), "Имеются неотправленные данные инвентаризации. Они будут потеряны, продолжить ?"));
            }
            else
            {
                App._mainWindowsVm._generalFrame.Navigate(new Pages.MarkScanPage(new MarkScanInventoryPageVm(true)));
            }
        }

        public void GoToMarkScanСontinue()
        {
            App._mainWindowsVm._generalFrame.Navigate(new Pages.MarkScanPage(new MarkScanInventoryPageVm(false)));
        }

        public void SendDatatoCvC()
        {
            App._mainWindowsVm._generalFrame.Navigate(new Pages.QuaerePage(new ViewModels.QuaereInventorySendDataVm(), "Подтвердите отправку инвентаризации на сервер"));
        }

    }
}
