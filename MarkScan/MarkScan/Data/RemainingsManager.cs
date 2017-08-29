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

    }
}
