using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkScan.Models
{
    public interface IMarkScanModel
    {
        List<ScanResult> ScanResults { get; }

        void HandleExciseStamp(string exciseStamp);
        bool ValidExciseStamp(string exciseStamp);
        bool SendToCvC();

        void ClearData();
    }
}
