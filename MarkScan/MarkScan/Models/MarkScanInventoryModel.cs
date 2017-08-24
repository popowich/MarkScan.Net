
using System;
using MarkScan.Data;

namespace MarkScan.Models
{
    public class MarkScanInventoryModel : MarkScanModelBase, IMarkScanModel
    {
        public MarkScanInventoryModel(bool newDatay) :
            base(newDatay)
        {

        }

        protected override bool _sendToCvC(ResultScanPosititon resultPositiones)
        {
            return Network.CvcOpenApi.GetClientApi().Remainings(resultPositiones);
        }

        protected override void SaveScanData(string mark, string alkCode)
        {
            DataBaseManager.GetManager().SaveInventoryData(mark, alkCode);
        }

        protected override void ReadScanData()
        {
            var datalist = DataBaseManager.GetManager().ReadInventoryData();

            foreach (var mas in datalist)
            {
                ScanResults.Add(new ScanResult() { ExciseStamp = mas[1].ToString(), AlcCode = mas[2].ToString() });
            }
        }

        public override void ClearScanData()
        {
            DataBaseManager.GetManager().ClearInventoryData();
        }
    }


}
