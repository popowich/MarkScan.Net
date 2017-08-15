using System;
using MarkScan.Pages;

namespace MarkScan.ViewModels
{
    public class OperationWriteOffMenuPageVm : OperationInventoryMenuBase, IOperationMenuVm
    {
        public void GoToMainMenuPage()
        {
            App._mainWindowsVm._generalFrame.Navigate(new Uri(@"pack://application:,,,/" + AppSettings.NameAssembly + ";component/Pages/MainMenuPage.xaml", UriKind.Absolute));
        }

        public void GoToMarkScanNew()
        {
            var model = new Models.MarkScanWriteOffModel(false);

            if (model.ScanResults.Count > 0)
            {
                App._mainWindowsVm._generalFrame.Navigate( new Pages.QuaerePage(new ViewModels.QuaereWriteOffMenuVm(), "Имеются неотправленные данные списания. Они будут потеряны, продолжить ?"));
            }
            else
            {
                App._mainWindowsVm._generalFrame.Navigate(new Pages.MarkScanPage(new MarkScanWriteOffPageVm(true)));
            }
        }

        public void GoToMarkScanСontinue()
        {
            App._mainWindowsVm._generalFrame.Navigate(new Pages.MarkScanPage(new MarkScanWriteOffPageVm(false)));
        }

        public void SendDatatoCvC()
        {
            App._mainWindowsVm._generalFrame.Navigate(new Pages.QuaerePage(new ViewModels.QuaereWriteOffSendDataVm(), "Подтвердите отправку списания на сервер"));
        }

    }
}
