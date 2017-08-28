using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkScan.Models
{
    public interface IMarkScanModel
    {
        List<ScanResult> ScanResults { get; }

        string HandleExciseMark(string exciseStamp);
        bool DeleteExciseMark(string exciseMark);
        bool ValidExciseMark(string exciseStamp);
        bool ValidExciseMarkForLength(string exciseStamp);
        bool SendToCvC();

        void ClearScanDataFormBase();
    }
}
