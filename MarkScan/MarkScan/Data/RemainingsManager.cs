using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkScan.Data
{
    internal class RemainingsManager
    {
        private static RemainingsManager _remainingsManager;

        internal event EventHandler StartUpdateRemainings;
        internal event EventHandler EndUpdateRemainings;

        internal static RemainingsManager GetManager()
        {
            return _remainingsManager ?? new RemainingsManager();
        }

        /// <summary>
        /// Обновить таблицу остатков
        /// </summary>
        internal void UpdateRemainings()
        {
            Task.Factory.StartNew(() =>
            {
                StartUpdateRemainings?.Invoke(this, EventArgs.Empty);

                try
                {
                    var result = Network.CvcOpenApi.GetClientApi().Remainings();

                    var dataBase = Data.DataBaseManager.GetManager();

                    lock (dataBase)
                    {
                        dataBase.DeleteAllRemainingProduct();

                        foreach (var item in result.Response.Items)
                        {
                            dataBase.SaveRemainingProduct(item);
                        }
                    }

                }
                catch (Exception ex)
                {
                   AppSettings.HandlerException(ex);
                }

                EndUpdateRemainings?.Invoke(this, EventArgs.Empty);
            });
        }

        internal Network.JsonWrapers.ProductionRemainings GetRemainingForAlcCode(string alcCode)
        {
            var dataBase = Data.DataBaseManager.GetManager();

            lock (dataBase)
            {
                var res = dataBase.ReadeRemainingProductForAlcCode(alcCode);

                if (res != null)
                {
                    var data = new Network.JsonWrapers.ProductionRemainings
                    {
                        Id = Convert.ToInt32(res[1]),
                        Position = Convert.ToInt32(res[2]),
                        FullName = Convert.ToString(res[3]),
                        ShortName = Convert.ToString(res[4]),
                        AlcCode = Convert.ToString(res[5]),
                        Capacity = Convert.ToDouble(res[6]),
                        AlcVolume = Convert.ToDouble(res[7]),
                        EgaisQuantity = Convert.ToDouble(res[8]),
                        RealQuantity = Convert.ToDouble(res[9]),
                        ProductVCode = Convert.ToInt32(res[10])
                    };

                    return data;

                }
            }

            return null;
        }
    }
}
