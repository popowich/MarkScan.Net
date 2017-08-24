
using System;
using MarkScan.Data;

namespace MarkScan.Models
{
    public class MarkScanWriteOffModel : MarkScanModelBase, IMarkScanModel
    {
        public override string FileDataPath
        {
            get { return AppSettings.CurrDir + "\\WriteOffData.txt"; }
        }

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
                ScanResults.Add(new ScanResult() { ExciseStamp = mas[0], AlcCode = mas[1] });
            }
        }

        public override void ClearScanData()
        {
            DataBaseManager.GetManager().ClearWriteOffData();
        }
    }

}
