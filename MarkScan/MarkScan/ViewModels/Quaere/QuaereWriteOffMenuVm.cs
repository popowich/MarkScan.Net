using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarkScan.Pages;

namespace MarkScan.ViewModels
{
    public class QuaereWriteOffMenuVm : IQuaereVm
    {
        public void HandleNoResult()
        {
            App._mainWindowsVm._generalFrame.GoBack();
        }

        public void HandleYesResult()
        {
            App._mainWindowsVm._generalFrame.Navigate(new Pages.MarkScanPage(new ViewModels.MarkScanWriteOffPageVm(true)));
        }

    }
}
