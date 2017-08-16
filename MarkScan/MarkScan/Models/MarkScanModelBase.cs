using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MarkScan.Models
{
    public abstract class MarkScanModelBase
    {
        /// <summary>
        /// Коллеция отсканированных данных
        /// </summary>
        public List<ScanResult> ScanResults { get; } = new List<ScanResult>();
        /// <summary>
        /// Путь к файлу данных
        /// </summary>
        public abstract string FileDataPath { get;}

        public MarkScanModelBase(bool newDatay)
        {
            if (newDatay)
                ClearData();
            else
                _readData();
        }


        public void HandleExciseStamp(string exciseStamp)
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
            _saveNewData(exciseStamp, alkCode10);
        }

        public bool ValidExciseStamp(string exciseStamp)
        {
            return !string.IsNullOrEmpty(exciseStamp) && exciseStamp.Length == 68 && ScanResults.FirstOrDefault(x=>x.ExciseStamp == exciseStamp) == null;
        }

        public bool ValidExciseStampForLength(string exciseStamp)
        {
            return !string.IsNullOrEmpty(exciseStamp) && exciseStamp.Length == 68;
        }

        protected void _saveNewData(string exciseStamp, string alkCode)
        {
            try
            {
                using (var stream = new StreamWriter(FileDataPath, true))
                {
                    stream.WriteLine(exciseStamp + ";" + alkCode);
                }
            }
            catch (Exception e)
            {
                AppSettings.HandlerException(e);
            }
        }

        protected void _readData()
        {
            try
            {
                if (!File.Exists(FileDataPath))
                    return;

                using (var stream = new StreamReader(FileDataPath))
                {
                    while (stream.EndOfStream == false)
                    {
                        string str = stream.ReadLine();
                        string[] mas = str.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                        if (mas.Length == 2)
                            ScanResults.Add(new ScanResult() { ExciseStamp = mas[0], AlcCode = mas[1] });

                    }
                }
            }
            catch (Exception e)
            {
                AppSettings.HandlerException(e);
            }
        }

        public void ClearData()
        {
            try
            {
                if (!File.Exists(FileDataPath))
                    return;

                using (var stream = new StreamWriter(FileDataPath, false))
                {

                }
            }
            catch (Exception e)
            {
                AppSettings.HandlerException(e);
            }
        }

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
