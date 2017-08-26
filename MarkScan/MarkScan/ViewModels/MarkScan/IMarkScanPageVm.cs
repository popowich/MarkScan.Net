using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkScan.ViewModels
{
    public interface IMarkScanPageVm
    {
        void SetOwnerPage(Pages.MarkScanPage markScanPage);
        void GoToOpearationMenuPage();
        void SetModeDeleteMark();
    }
}
