using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkScan.Models
{
    public interface IMarkScanPageModel
    {
        List<ScanResult> ScanResults { get; }

        void HandleExciseStamp(string exciseStamp);
        bool ValidExciseStamp(string exciseStamp);
        void SendToCvC();
    }
}
