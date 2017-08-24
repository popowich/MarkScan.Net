using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MarkScan.Data;

namespace MarkScan.Models
{
    public abstract class MarkScanModelBase
    {
        /// <summary>
        /// Коллеция отсканированных данных
        /// </summary>
        public List<ScanResult> ScanResults { get; } = new List<ScanResult>();

        public MarkScanModelBase(bool newDatay)
        {
            try
            {
                if (newDatay)
                    ClearScanData();
                else
                    ReadScanData();
            }
            catch (Exception e)
            {
               AppSettings.HandlerException(e);
            }
        }


        public void HandleExciseStamp(string exciseStamp)
        {
            try
            {
                string alkCode36 = exciseStamp.Substring(3, 16);
                string alkCode10 = Tools.Convertor36To10String.Convert(alkCode36);

                if (alkCode10.Length < 19)
                {
                    int difference = 19 - alkCode10.Length;
                    for (int i = 1; i <= difference; i++)
                        alkCode10 = "0" + alkCode10;
                }

                ScanResults.Add(new ScanResult() { ExciseStamp = exciseStamp, AlcCode = alkCode10 });
                SaveScanData(exciseStamp, alkCode10);
            }
            catch (Exception e)
            {
              AppSettings.SaveDebug("Ошибка обработки марки: " + exciseStamp);
              AppSettings.HandlerException(e);
            }
   
        }

        public bool ValidExciseStamp(string exciseStamp)
        {
            return !string.IsNullOrEmpty(exciseStamp) && exciseStamp.Length == 68 && ScanResults.FirstOrDefault(x=>x.ExciseStamp == exciseStamp) == null;
        }

        public bool ValidExciseStampForLength(string exciseStamp)
        {
            return !string.IsNullOrEmpty(exciseStamp) && exciseStamp.Length == 68;
        }

        protected abstract void SaveScanData(string exciseStamp, string alkCode);

        protected abstract void ReadScanData();

        public abstract void ClearScanData();

        public bool SendToCvC()
        {
            var resultPositiones = new ResultScanPosititon();
            List<ResultScan> listScan = new List<ResultScan>();

            var phoneGroups = ScanResults.GroupBy(p => p.AlcCode)
                    .Select(g => new { Name = g.Key, Count = g.Count() });

            foreach (var data in phoneGroups)
                listScan.Add(new ResultScan() { AlcCode = data.Name, Quantity = data.Count });

            resultPositiones.Positions = listScan.ToArray();

            return _sendToCvC(resultPositiones);
        }

        protected abstract bool _sendToCvC(ResultScanPosititon resultPositiones);
    }
}
