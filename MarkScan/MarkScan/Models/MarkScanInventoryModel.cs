
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

        protected override void SaveExciseMarkFormBase(string mark, string alkCode)
        {
            DataBaseManager.GetManager().SaveInventoryMark(mark, alkCode);
        }

        protected override void ReadExciseMarkFormBase()
        {
            var datalist = DataBaseManager.GetManager().ReadInventoryMark();

            foreach (var mas in datalist)
            {
                ScanResults.Add(new ScanResult() { ExciseStamp = mas[1].ToString(), AlcCode = mas[2].ToString() });
            }
        }

        protected override void DeleteExciseMarkFormBase(string exciseStamp)
        {
            DataBaseManager.GetManager().DeleteInventoryMark(exciseStamp);
        }

        public override void ClearScanDataFormBase()
        {
            DataBaseManager.GetManager().ClearInventoryMark();
        }
    }


}
