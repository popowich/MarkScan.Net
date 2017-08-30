
using System;
using System.Collections.Generic;
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
            var dataBase = DataBaseManager.GetManager();
            lock (dataBase)
            {
                dataBase.SaveWriteOffMark(mark, alkCode);
            }
        }

        protected override void ReadExciseMarkFormBase()
        {
            var dataBase = DataBaseManager.GetManager();
            List<object[]> datalist = null;

            lock (dataBase)
            {
                datalist = dataBase.ReadWriteOffMark();
            }

            foreach (var mas in datalist)
            {
                ScanResults.Add(new ScanResult() { ExciseStamp = mas[1].ToString(), AlcCode = mas[2].ToString() });
            }
        }

        protected override void DeleteExciseMarkFormBase(string exciseStamp)
        {
            var dataBase = DataBaseManager.GetManager();

            lock (dataBase)
            {
                dataBase.DeleteWriteOffMark(exciseStamp);
            }
        }

        public override void ClearScanDataFormBase()
        {
            var dataBase = DataBaseManager.GetManager();

            lock (dataBase)
            {
                dataBase.DeleteAllWriteOffMark();
            }

        }
    }

}
