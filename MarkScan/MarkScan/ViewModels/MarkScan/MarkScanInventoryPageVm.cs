﻿
namespace MarkScan.ViewModels
{
    public class MarkScanInventoryPageVm : MarkScanPageBaseVm, IMarkScanPageVm
    {
        public MarkScanInventoryPageVm(bool newInventory) :
            base(new Models.MarkScanInventoryModel(newInventory))
        {

        }

        public void SetOwnerPage(Pages.MarkScanPage markScanPage)
        {
            base.SetMyPage(markScanPage);

            _markScanPage.nameOperation.Content = "Инвентаризация";
        }

        public void GoToOpearationMenuPage()
        {
            App._mainWindowsVm._generalFrame.Navigate(new Pages.OperationMenuPage(new ViewModels.OperationInventoryMenuPageVm()));
        }
    }
}
