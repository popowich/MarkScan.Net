using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkScan.Models
{
    public interface IMarkScanModel
    {
        List<ScanResult> ScanResults { get; }

        string HandleExciseMark(string mark);
        bool DeleteExciseMark(string mark);
        bool ValidExciseMark(string mark);
        bool ValidExciseMarkForLength(string mark);
        bool IsDuplicationMark(string mark);
        bool SendToCvC();
        string GetDescriptionAlcCode(string alcCode);

        void ClearScanDataFormBase();
    }
}
