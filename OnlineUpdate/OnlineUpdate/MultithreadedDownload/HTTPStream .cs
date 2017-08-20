using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OnlineUpdate.MultithreadedDownload
{
    /// <summary>
    /// Реализация HTTP потока
    /// </summary>
    public class HTTPStream : Stream, IDisposable
    {
        private Stream stream;
        private WebResponse resp;
        private HttpWebRequest req;

        private int lengthPart = 0;
        long position = 0;
        long? length;

        public Uri Url { get; private set; }
        public override bool CanRead { get { return true; } }
        public override bool CanWrite { get { return false; } }
        public override bool CanSeek { get { return true; } }

        /// <summary>
        /// Позиция в потоке
        /// </summary>
        public override long Position
        {
            get { return position; }
            set { position = value; }
        }
        /// <summary>
        /// Длинна потока
        /// </summary>
        public override long Length
        {
            get
            {
                if (length == null)
                {
                    length = GetLengthStream(this.Url);
                }
                return length.Value;
            }
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="Url"></param>
        public HTTPStream(Uri Url, int _lengthPart)
        {
            this.Url = Url;
            this.lengthPart = _lengthPart;
        }
        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (this.stream != null)
                this.stream.Dispose();

            base.Dispose(disposing);
        }

        /// <summary>
        /// Установить длинну
        /// </summary>
        /// <param name="value"></param>
        public override void SetLength(long value)
        {
            resp.Close();
            stream.Close();
            throw new NotImplementedException();
        }
        /// <summary>
        /// Прочитать блок данных
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="_offset"></param>
        /// <param name="_count"></param>
        /// <returns></returns>
        public override int Read(byte[] buffer, int _offset, int _count)
        {
            if (stream == null)
            {
                req = (HttpWebRequest)HttpWebRequest.Create(Url);
                //req.Timeout = 7000;

                req.AddRange(Position, Position + lengthPart - 1);
                resp = req.GetResponse();
                stream = resp.GetResponseStream();
            }

            stream.ReadTimeout = 15000;
            int nread = stream.Read(buffer, _offset, _count);

            return nread;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        public override long Seek(long pos, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.End:
                    Position = Length + pos;
                    break;
                case SeekOrigin.Begin:
                    Position = pos;
                    break;
                case SeekOrigin.Current:
                    Position += pos;
                    break;
            }
            return Position;
        }
        /// <summary>
        /// 
        /// </summary>
        public override void Flush() { }
        /// <summary>
        /// 
        /// </summary>
        new void Dispose()
        {
            base.Dispose();
            if (stream != null)
            {
                stream.Dispose();
                stream = null;
            }
            if (resp != null)
            {
                resp.Close();
                resp = null;
            }
        }

        #region Static methods

        /// <summary>
        /// Получить длину потока
        /// </summary>
        /// <param name="_uri"></param>
        /// <returns></returns>
        public static long GetLengthStream(Uri _uri)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(_uri);
            request.Method = "HEAD";
            using (WebResponse rp = request.GetResponse())
            {
                return rp.ContentLength;
            }
        }

        #endregion
    }
}
