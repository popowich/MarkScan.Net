using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineUpdate.MultithreadedDownload
{
    /// <summary>
    /// Реализация параметров события StartDownloadFile
    /// </summary>
    public class StartDownloadEventArgs:EventArgs
    {
        /// <summary>
        /// Имя файла
        /// </summary>
        public string NameFile { get; set; }
        /// <summary>
        /// Размер файла
        /// </summary>
        public int SizeFile { get; set; }
        /// <summary>
        /// Уже загружено на данный момент
        /// </summary>
        public int NowDownloadCount { get; set; }
        /// <summary>
        /// Количество потоков
        /// </summary>
        public int KolThread { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public StartDownloadEventArgs(string _nameFile, int _sizeFile, int _nowDownloadCount, int _kolThread)
        {
            this.NameFile = _nameFile;
            this.SizeFile = _sizeFile;
            this.NowDownloadCount = _nowDownloadCount;
            this.KolThread = _kolThread;
        }
    }
}
