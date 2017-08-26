
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

        protected override void SaveExciseMarkFormBase(string mark, string alkCode)
        {
            DataBaseManager.GetManager().SaveWriteOffMark(mark, alkCode);
        }

        protected override void ReadExciseMarkFormBase()
        {
            var datalist = DataBaseManager.GetManager().ReadWriteOffMark();

            foreach (var mas in datalist)
            {
                ScanResults.Add(new ScanResult() { ExciseStamp = mas[1].ToString(), AlcCode = mas[2].ToString() });
            }
        }

        protected override void DeleteExciseMarkFormBase(string exciseStamp)
        {
            DataBaseManager.GetManager().DeleteWriteOffMark(exciseStamp);
        }

        public override void ClearScanDataFormBase()
        {
            DataBaseManager.GetManager().ClearWriteOffMark();
        }
    }

}
