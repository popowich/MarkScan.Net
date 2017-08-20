using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineUpdate.MultithreadedDownload
{
    /// <summary>
    /// Класс реализует поток скачивания блока файла
    /// </summary>
    public class MultiDownloadFilePart : IDisposable
    {
        /// <summary>
        /// Размер буфера чтения 81920
        /// </summary>
        private const int sizeBuffRead = 89460;
        /// <summary>
        /// Буфер чтения 
        /// </summary>
        private byte[] bufRead = null;
        /// <summary>
        /// Общий счетчик чтения потока
        /// </summary>
        private int readCount;
        /// <summary>
        /// Ссылка скачивания файла
        /// </summary>
        private Uri linkDownload = null;
        /// <summary>
        /// Загловок опсиания области
        /// </summary>
        private HeaderPartTempFile headerPart;


        /// <summary>
        /// Ссылка скачивания файла
        /// </summary>
        public Uri LinkDownload
        {
            get { return linkDownload; }
        }
        /// <summary>
        /// Стратовая позиция области чтения
        /// </summary>
        public int StartPositionHttpStream { get; private set; }
        /// <summary>
        /// Стратовая позиция области записи
        /// </summary>
        public int StartPositionFileStream { get; private set; }
        /// <summary>
        /// Длинна области
        /// </summary>
        public int LengthPart { get; private set; }
        /// <summary>
        /// Размер загруженной области
        /// </summary>
        public int SizeDownloaded { get; private set; }
        /// <summary>
        /// Загловок опсиания области
        /// </summary>
        public HeaderPartTempFile HeaderPart
        {
            get
            {
                return headerPart;
            }
        }

        #region Eventes

        /// <summary>
        /// Событие нала загрузки области файла
        /// </summary>
        public event EventHandler BeginDownloadEvent;
        /// <summary>
        /// Событие окончания загрузки области файла
        /// </summary>
        public event EventHandler EndDownloadEvent;
        /// <summary>
        /// Событие загрузки блока данных
        /// </summary>
        public event EventHandler<DownloadFilePartReadEventArgs> ReadEvent;

        /// <summary>
        /// Вызвать обработчик
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        public void RunBeginDownloadEvent(object _sender, EventArgs _e)
        {
            var ev = this.BeginDownloadEvent;
            if (ev != null)
            {
                ev(_sender, _e);
            }
        }
        /// <summary>
        /// Вызвать обработчик
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        public void RunEndDownloadEvent(object _sender, EventArgs _e)
        {
            var ev = this.EndDownloadEvent;
            if (ev != null)
            {
                ev(_sender, _e);
            }
        }
        /// <summary>
        /// Вызвать обработчик
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        public void RunReadEvent(object _sender, DownloadFilePartReadEventArgs _e)
        {
            var ev = this.ReadEvent;
            if (ev != null)
            {
                ev(_sender, _e);
            }
        }

        #endregion

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="_linkDownload"></param>
        public MultiDownloadFilePart(Uri _linkDownload, int _startPositionHttpStream, int _startPositionFileStream, int _lengthPart)
        {
            this.linkDownload = _linkDownload;

            this.StartPositionHttpStream = _startPositionHttpStream;
            this.StartPositionFileStream = _startPositionFileStream;
            this.LengthPart = _lengthPart;

            this.headerPart = new HeaderPartTempFile()
            {
                StartPositionHttpStream = this.StartPositionHttpStream,
                StartPositionFileStream = this.StartPositionFileStream,
                Length = this.LengthPart

            };
        }
        /// <summary>
        /// Конструктор на основании заголовка описания области
        /// </summary>
        /// <param name="_linkDownload"></param>
        /// <param name="_header"></param>
        public MultiDownloadFilePart(Uri _linkDownload, HeaderPartTempFile _header)
        {
            this.linkDownload = _linkDownload;

            this.StartPositionHttpStream = _header.StartPositionHttpStream + _header.SizeDownloaded;
            this.StartPositionFileStream = _header.StartPositionFileStream + _header.SizeDownloaded;
            this.LengthPart = _header.Length;
            this.SizeDownloaded = _header.SizeDownloaded;
            this.readCount = _header.SizeDownloaded;

            this.headerPart = _header;
        }
        /// <summary>
        /// Освободить ресурсы
        /// </summary>
        public void Dispose()
        {
            if (this.BeginDownloadEvent != null)
                foreach (EventHandler ev in this.BeginDownloadEvent.GetInvocationList())
                    this.BeginDownloadEvent -= ev;

            if (this.EndDownloadEvent != null)
                foreach (EventHandler ev in this.EndDownloadEvent.GetInvocationList())
                    this.EndDownloadEvent -= ev;

            if (this.ReadEvent != null)
                foreach (EventHandler<DownloadFilePartReadEventArgs> ev in this.ReadEvent.GetInvocationList())
                    this.ReadEvent -= ev;
        }

        /// <summary>
        /// Начать загрузку блока файла
        /// </summary>
        public Task DownloadAsync(CancellationToken _cancaleToken)
        {
            Task task = Task.Factory.StartNew(() =>
            {
                HTTPStream httpStream = null;
                try
                {
                    this.RunBeginDownloadEvent(this, EventArgs.Empty);

                    httpStream = new HTTPStream(this.LinkDownload, this.LengthPart);
                    int sizeBuff = sizeBuffRead <= this.LengthPart && sizeBuffRead <= httpStream.Length ? sizeBuffRead : this.LengthPart;

                    int setOff = 0;
                    //Цикл чтения потока
                    while (readCount < this.LengthPart)
                    {
                        bufRead = new byte[sizeBuff];

                        httpStream.Position = this.StartPositionHttpStream + setOff;
                        int realReadLength = httpStream.Read(bufRead, 0, bufRead.Length);

                        byte[] buffOut = new byte[realReadLength];
                        if (bufRead.Length > realReadLength)
                            Array.Copy(bufRead, 0, buffOut, 0, realReadLength);
                        else
                            buffOut = bufRead;

                        this.RunReadEvent(this, new DownloadFilePartReadEventArgs(this, buffOut, realReadLength));
                        readCount += realReadLength;
                        setOff += realReadLength;

                        //Отмена выполнения
                        if (_cancaleToken.IsCancellationRequested)
                            _cancaleToken.ThrowIfCancellationRequested();
                    }

                    this.RunEndDownloadEvent(this, EventArgs.Empty);
                }
                catch (OperationCanceledException)
                {

                }
                catch (Exception ex)
                {
                    throw new Exception("Error download file part", ex);
                }
                finally
                {
                    if (httpStream != null)
                        httpStream.Dispose();
                }

            }, _cancaleToken);

            return task;
        }
    }
}
