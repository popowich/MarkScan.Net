using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkScan.ViewModels
{
    public class MarkScanWriteOffPageVm : MarkScanPageBaseVm, IMarkScanPageVm
    {
        public MarkScanWriteOffPageVm(bool newData) :
            base(new Models.MarkScanWriteOffModel(newData))
        {

        }

        public void SetOwnerPage(Pages.MarkScanPage markScanPage)
        {
            base.SetMyPage(markScanPage);

            _markScanPage.nameOperation.Content = "Списание";
        }

        public void GoToOpearationMenuPage()
        {
            App._mainWindowsVm._generalFrame.Navigate(new Pages.OperationMenuPage(new ViewModels.OperationWriteOffMenuPageVm()));
        }
    }
}
