using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineUpdate.UpdaterEventArgs
{
    /// <summary>
    /// Реализация параметров события BeginDownloadFilesEven
    /// </summary>
    public class BeginDownloadFilesEventArgs : EventArgs
    {
        /// <summary>
        /// Количесвто файлов
        /// </summary>
        public int KolFiles { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public BeginDownloadFilesEventArgs(int _kolFiles)
        {
            this.KolFiles = _kolFiles;
        }
    }
}
