﻿
namespace MarkScan.ViewModels
{
    public class QuaereWriteOffSendDataVm : IQuaereVm
    {
        public void HandleNoResult()
        {
            App._mainWindowsVm._generalFrame.GoBack();
        }

        public void HandleYesResult()
        {
            App._mainWindowsVm._generalFrame.Navigate(new Pages.SendDataToCvCPage(new ViewModels.SendDataWriteOffToCvCVm()));
        }
    }
}
