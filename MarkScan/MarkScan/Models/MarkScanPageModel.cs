using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;

namespace MarkScan.Models
{
    public class MarkScanPageInventoryModel: IMarkScanPageModel
    {
        /// <summary>
        /// Коллеция отсканированных данных
        /// </summary>
        public List<ScanResult> ScanResults { get; } = new List<ScanResult>();

        public MarkScanPageInventoryModel(bool newInventory)
        {
            if (newInventory)
                _clearData();
            else
                _readData();
        }

        /// <summary>
        /// Обработать акцизную марку
        /// </summary>
        /// <param name="exciseStamp"></param>
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
            return !string.IsNullOrEmpty(exciseStamp) && exciseStamp.Length == 68;
        }

        private void _saveNewData(string exciseStamp, string alkCode)
        {
            try
            {
                using (var stream = new StreamWriter(AppSettings.CurrDir + "\\InventoryData.txt", true))
                {
                    stream.WriteLine(exciseStamp + ";" + alkCode);
                }
            }
            catch (Exception e)
            {
                AppSettings.HandlerException(e);
            }
        }

        private void _readData()
        {
            try
            {
                if (!File.Exists(AppSettings.CurrDir + "\\InventoryData.txt"))
                    return;

                using (var stream = new StreamReader(AppSettings.CurrDir + "\\InventoryData.txt"))
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

        private void _clearData()
        {
            try
            {
                if (!File.Exists(AppSettings.CurrDir + "\\InventoryData.txt"))
                    return;

                using (var stream = new StreamWriter(AppSettings.CurrDir + "\\InventoryData.txt", false))
                {

                }
            }
            catch (Exception e)
            {
                AppSettings.HandlerException(e);
            }
        }

        public void SendToCvC()
        {
            if (ScanResults.Count == 0)
            {
                MessageBox.Show("Отправлять нечего!");
                return;
            }

            var resulP = new ResultScanPosititon();

            List<ResultScan> ss = new List<ResultScan>();
            var phoneGroups = ScanResults.GroupBy(p => p.AlcCode)
                    .Select(g => new { Name = g.Key, Count = g.Count() });

            foreach (var data in phoneGroups)
            {
                ss.Add(new ResultScan() { AlcCode = data.Name, Quantity = data.Count });
            }

            resulP.Positions = ss.ToArray();

            try
            {
                if (Network.CvcOpenApi.GetClientApi().Remainings(resulP))
                    MessageBox.Show("Отправлено успешно!");
                else
                    MessageBox.Show("Отправлено не удалось!");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                AppSettings.HandlerException(e);
            }

        }
    }

    public class ScanResult
    {
        /// <summary>
        /// Акцизная марка
        /// </summary>
        internal string ExciseStamp { get; set; }
        /// <summary>
        /// Алкод
        /// </summary>
        internal string AlcCode { get; set; }
    }
}
