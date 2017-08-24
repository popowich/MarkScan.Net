
using System;
using MarkScan.Data;

namespace MarkScan.Models
{
    public class MarkScanWriteOffModel : MarkScanModelBase, IMarkScanModel
    {
        public MarkScanWriteOffModel(bool newDatay) :
            base(newDatay)
        {

        }

        protected override bool _sendToCvC(ResultScanPosititon resultPositiones)
        {
            return Network.CvcOpenApi.GetClientApi().Writeoff(resultPositiones);
        }

        protected override void SaveScanData(string mark, string alkCode)
        {
            DataBaseManager.GetManager().SaveWriteOffData(mark, alkCode);
        }

        protected override void ReadScanData()
        {
            var datalist = DataBaseManager.GetManager().ReadWriteOffData();

            foreach (var mas in datalist)
            {
                ScanResults.Add(new ScanResult() { ExciseStamp = mas[1].ToString(), AlcCode = mas[2].ToString() });
            }
        }

        public override void ClearScanData()
        {
            DataBaseManager.GetManager().ClearWriteOffData();
        }
    }

}
