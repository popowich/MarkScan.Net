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
                    ClearScanDataFormBase();
                else
                    ReadExciseMarkFormBase();
            }
            catch (Exception e)
            {
                AppSettings.HandlerException(e);
            }
        }

        /// <summary>
        /// Обработать поступление марки
        /// </summary>
        /// <param name="exciseMark"></param>
        public string HandleExciseMark(string exciseMark)
        {
            try
            {
                string alkCode36 = exciseMark.Substring(3, 16);
                string alkCode10 = Tools.Convertor36To10String.Convert(alkCode36);

                if (alkCode10.Length < 19)
                {
                    int difference = 19 - alkCode10.Length;
                    for (int i = 1; i <= difference; i++)
                        alkCode10 = "0" + alkCode10;
                }

                ScanResults.Add(new ScanResult() { ExciseStamp = exciseMark, AlcCode = alkCode10 });
                SaveExciseMarkFormBase(exciseMark, alkCode10);

                return alkCode10;
            }
            catch (Exception e)
            {
                AppSettings.SaveDebug("Ошибка обработки марки: " + exciseMark);
                AppSettings.HandlerException(e);
            }

            return null;
        }
        /// <summary>
        /// Удалить поступившую марку
        /// </summary>
        /// <param name="exciseMark"></param>
        /// <returns></returns>
        public bool DeleteExciseMark(string exciseMark)
        {
            var scanData = ScanResults.FirstOrDefault(x => x.ExciseStamp == exciseMark);
            if (scanData != null)
            {
                ScanResults.Remove(scanData);
                DeleteExciseMarkFormBase(exciseMark);

                return true;
            }

            return false;
        }
        /// <summary>
        /// Проверить валидность марки
        /// </summary>
        /// <param name="exciseStamp"></param>
        /// <returns></returns>
        public bool ValidExciseMark(string exciseStamp)
        {
            return !string.IsNullOrEmpty(exciseStamp) && exciseStamp.Length == 68 && ScanResults.FirstOrDefault(x => x.ExciseStamp == exciseStamp) == null;
        }
        /// <summary>
        /// Проверить валидность марки по динне
        /// </summary>
        /// <param name="exciseStamp"></param>
        /// <returns></returns>
        public bool ValidExciseMarkForLength(string exciseStamp)
        {
            return !string.IsNullOrEmpty(exciseStamp) && exciseStamp.Length == 68;
        }

        protected abstract void SaveExciseMarkFormBase(string exciseStamp, string alkCode);

        protected abstract void DeleteExciseMarkFormBase(string exciseStamp);

        protected abstract void ReadExciseMarkFormBase();

        public abstract void ClearScanDataFormBase();

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
