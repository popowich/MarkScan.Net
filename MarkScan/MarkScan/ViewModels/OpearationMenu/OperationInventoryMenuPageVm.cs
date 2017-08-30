using System;

namespace MarkScan.ViewModels
{
    public class OperationInventoryMenuPageVm : OperationInventoryMenuBase, IOperationMenuVm
    {
        public OperationInventoryMenuPageVm()
        {
            App._mainWindowsVm._mainWindow.Title = "Mark Scan.Net - инвентаризация";
        }

        public void GoToMainMenuPage()
        {
            App._mainWindowsVm._mainWindow.Title = "Mark Scan.Net";
            App._mainWindowsVm._generalFrame.Navigate(new Uri(@"pack://application:,,,/" + AppSettings.NameAssembly + ";component/Pages/MainMenuPage.xaml", UriKind.Absolute));
        }

        public void GoToMarkScanNew()
        {
            bool res = false;

            try
            {
                res = Data.DataBaseManager.GetManager().ExistInventoryMark();
            }
            catch (Exception e)
            {
                AppSettings.HandlerException(e);
            }

            if (res)
            {
                App._mainWindowsVm._generalFrame.Navigate(new Pages.QuaerePage(new ViewModels.QuaereInventiryMenuVm(), "Имеются неотправленные данные инвентаризации. Они будут потеряны, продолжить ?"));
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
