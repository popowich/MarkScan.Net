
using System;
using MarkScan.Data;

namespace MarkScan.Models
{
    public class MarkScanInventoryModel: MarkScanModelBase, IMarkScanModel
    {
        public override string FileDataPath
        {
            get { return AppSettings.CurrDir + "\\InventoryData.txt"; }
        }

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
                ScanResults.Add(new ScanResult() { ExciseStamp = mas[0], AlcCode = mas[1] });
            }
        }

        public override void ClearScanData()
        {
            DataBaseManager.GetManager().ClearInventoryData();
        }
    }


}
