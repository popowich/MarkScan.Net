using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkScan.ViewModels
{
    public interface IOperationMenuVm
    {
        void GoToMainMenuPage();
        void GoToMarkScanNew();
        void GoToMarkScanСontinue();
        void SendDatatoCvC();
    }
}
