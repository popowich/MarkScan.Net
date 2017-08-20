using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineUpdate.MultithreadedDownload
{
    /// <summary>
    /// Реализация парамтеров события SaveBuff
    /// </summary>
    public class SaveBuffEventArgs:EventArgs
    {
        /// <summary>
        /// Размер записываемого буфера
        /// </summary>
        public int SizeWrite
        {
            get;
            set;
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="_sizeWrite"></param>
        public SaveBuffEventArgs(int _sizeWrite)
        {
            this.SizeWrite = _sizeWrite;
        }
    }
}
