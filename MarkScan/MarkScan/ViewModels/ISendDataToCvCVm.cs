using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkScan.ViewModels
{
    public interface ISendDataToCvCVm
    {
        void SetPage(Pages.SendDataToCvCPage page);
        void SendData();
    }
}
