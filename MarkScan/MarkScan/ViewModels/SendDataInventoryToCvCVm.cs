using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkScan.ViewModels
{
    public class SendDataInventoryToCvCVm : ISendDataToCvCVm
    {
        private Pages.SendDataToCvCPage _page;

        public void SendData()
        {
            try
            {
                new Models.MarkScanPageInventoryModel(false).SendToCvC();

                _page.textBlock.Text = "Успех!";

                new Models.MarkScanPageInventoryModel(true);

                System.Threading.Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                _page.textBlock.Text = e.Message;
            }

            App._mainWindowsVm._generalFrame.Navigate(new Pages.OperationMenuPage(new InventoryOpeationMenuPageVm()));
        }

        public void SetPage(Pages.SendDataToCvCPage page)
        {
            _page = page;
        }
    }
}
