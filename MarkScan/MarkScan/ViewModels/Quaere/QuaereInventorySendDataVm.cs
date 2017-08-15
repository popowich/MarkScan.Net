using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkScan.ViewModels
{
    public class QuaereInventorySendDataVm : IQuaereVm
    {
        public void HandleNoResult()
        {
            App._mainWindowsVm._generalFrame.GoBack();
        }

        public void HandleYesResult()
        {
            App._mainWindowsVm._generalFrame.Navigate(new Pages.SendDataToCvCPage(new ViewModels.SendDataInventoryToCvCVm()));
        }
    }
}
