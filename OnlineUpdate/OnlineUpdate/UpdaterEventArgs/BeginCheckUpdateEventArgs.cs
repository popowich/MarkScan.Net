using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineUpdate.UpdaterEventArgs
{
    public class BeginCheckUpdateEventArgs:EventArgs
    {
        /// <summary>
        /// Количество файлов
        /// </summary>
        public int KolFiles { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="_kolFiles"></param>
        public BeginCheckUpdateEventArgs(int _kolFiles)
        {
            this.KolFiles = _kolFiles;
        }
    }
}
