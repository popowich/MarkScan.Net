using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineUpdate.MultithreadedDownload
{
    public class DownloadFilePartReadEventArgs :EventArgs
    {
        /// <summary>
        /// Источник
        /// </summary>
        public MultiDownloadFilePart Sender { get; set; }
        /// <summary>
        /// Считанная длинна
        /// </summary>
        public int ReadLength { get; set; }
        /// <summary>
        /// Буфер считанных днных
        /// </summary>
        public byte[] BuffRead { get; set; }

        /// <summary>
        /// Основной конструктор
        /// </summary>
        /// <param name="_buff"></param>
        public DownloadFilePartReadEventArgs(MultiDownloadFilePart _sender, byte[] _buff, int _lengthRead)
        {
            this.Sender = _sender;
            this.BuffRead = _buff;
            this.ReadLength = _lengthRead;
        }
    }
}
