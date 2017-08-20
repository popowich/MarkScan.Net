using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CreatorReleases
{
    /// <summary>
    /// Загрузчик файлов релиза на FTP
    /// </summary>
    public class PublicationOnFtp : ITaskExecution, IDisposable
    {
        /// <summary>
        /// FTP клиент
        /// </summary>
        private FtpClient ftpClient = null;
        /// <summary>
        /// Адрес сервера
        /// </summary>
        private string url;
        /// <summary>
        /// Порт соединения
        /// </summary>
        private int port;
        /// <summary>
        /// Имя пользователя
        /// </summary>
        private string userName;
        /// <summary>
        /// Пароль
        /// </summary>
        private string password;
        /// <summary>
        /// Корневая дирректория 
        /// </summary>
        public string RootDirrFtp { get; set; }
        /// <summary>
        /// Корень релиза
        /// </summary>
        public FileRelease RootRelease { get; set; }

        #region Eventes

        /// <summary>
        /// Событие начала загрузки данных
        /// </summary>
        public event EventHandler<BeginUpLoadEventArgs> BeginUpLoadEvent;
        /// <summary>
        /// Событие окончания загрузки данных
        /// </summary>
        public event EventHandler EndUpLoadEvent;
        /// <summary>
        /// Событие успешной загруки файла
        /// </summary>
        public event EventHandler<UpLoadFileEventArgs> UpLoadFileEvent;
        /// <summary>
        /// Событие ошибки
        /// </summary>
        public event ErrorEventHandler ErrorEvent;

        /// <summary>
        /// Вызвать обработку события BeginUpLoadEvent
        /// </summary>
        /// <param name="_sendre"></param>
        /// <param name="_e"></param>
        public void OnBeginUpLoadEvent(object _sendre, BeginUpLoadEventArgs _e)
        {
            var evHandler = this.BeginUpLoadEvent;
            if (evHandler != null)
                evHandler(_sendre, _e);
        }
        /// <summary>
        /// Вызвать обработку события EndUpLoadEvent
        /// </summary>
        /// <param name="_sendre"></param>
        /// <param name="_e"></param>
        public void OnEndUpLoadEvent(object _sendre, EventArgs _e)
        {
            var evHandler = this.EndUpLoadEvent;
            if (evHandler != null)
                evHandler(_sendre, _e);
        }
        /// <summary>
        /// Вызвать обработку события UpLoadFileEvent
        /// </summary>
        /// <param name="_sendre"></param>
        /// <param name="_e"></param>
        public void OnUpLoadFileEvent(object _sendre, UpLoadFileEventArgs _e)
        {
            var evHandler = this.UpLoadFileEvent;
            if (evHandler != null)
                evHandler(_sendre, _e);
        }
        /// <summary>
        /// Вызвать обработку события ErrorEvent
        /// </summary>
        /// <param name="_sendre"></param>
        /// <param name="_e"></param>
        public void OnErrorEvent(object _sendre, ErrorEventArgs _e)
        {
            var evHandler = this.ErrorEvent;
            if (evHandler != null)
                evHandler(_sendre, _e);
        }

        #endregion

        /// <summary>
        /// Конструктор 
        /// </summary>
        public PublicationOnFtp(string _url, int _port, string _userName, string _password, FileRelease _start, string _rootDirrFtp)
        {
            this.url = _url;
            this.port = _port;
            this.userName = _userName;
            this.password = _password;

            this.RootDirrFtp = _rootDirrFtp;
            this.RootRelease = _start;
        }
        /// <summary>
        /// Освободить ресурсы
        /// </summary>
        public void Dispose()
        {
            if (this.ftpClient != null)
                this.ftpClient.Dispose();

            if (this.BeginUpLoadEvent != null)
                foreach (EventHandler<BeginUpLoadEventArgs> ev in this.BeginUpLoadEvent.GetInvocationList())
                    this.BeginUpLoadEvent -= ev;

            if (this.EndUpLoadEvent != null)
                foreach (EventHandler ev in this.EndUpLoadEvent.GetInvocationList())
                    this.EndUpLoadEvent -= ev;

            if (this.UpLoadFileEvent != null)
                foreach (EventHandler<UpLoadFileEventArgs> ev in this.UpLoadFileEvent.GetInvocationList())
                    this.UpLoadFileEvent -= ev;

            if (this.ErrorEvent != null)
                foreach (ErrorEventHandler ev in this.ErrorEvent.GetInvocationList())
                    this.ErrorEvent -= ev;
        }

        /// <summary>
        /// Начать загрузку
        /// </summary>
        /// <param name="_start"></param>
        public Task Begin()
        {
        
            Task task = Task.Factory.StartNew(() =>
            {
                this.OnBeginUpLoadEvent(this, new BeginUpLoadEventArgs(GetNumberOfFiles(this.RootRelease) - 1));

                try
                {
                    this.ftpClient = new FtpClient(this.url, this.port, this.userName, this.password);
                    this.ftpClient.Connect();
                }
                catch (Exception ex)
                {
                    this.OnErrorEvent(this, new ErrorEventArgs(ex));
                }

                if (this.ftpClient.IsConnected)
                {
                    //Ищем и удаляем существующую корневую дирректорию 
                    BytesRoad.Net.Ftp.FtpItem[] dirres = this.ftpClient.GetDirectoryList();
                    foreach (BytesRoad.Net.Ftp.FtpItem itm in dirres)
                    {
                        if (itm.ItemType == BytesRoad.Net.Ftp.FtpItemType.Directory && itm.Name == this.RootDirrFtp)
                        {
                            this.ftpClient.RecursiveDeleteDirectory(this.RootDirrFtp);
                            break;
                        }
                    }

                    //Создаем корневую дирректорию
                    this.ftpClient.CreateDirectory(this.RootDirrFtp);
                    //Переходи м нее
                    this.ftpClient.ChangeDirectory(this.RootDirrFtp);
                    //Загружаем весь контент
                    Action<FileRelease, bool> Upload = null;
                    Upload = (FileRelease _startDir, bool _createDirr) =>
                    {
                        //Создаем дирректорию
                        if (_createDirr)
                        {
                            this.ftpClient.CreateDirectory(_startDir.FileNameOriginal);
                            this.ftpClient.ChangeDirectory(_startDir.FileNameOriginal);
                            this.OnUpLoadFileEvent(this, new UpLoadFileEventArgs(_startDir));
                        }

                        //Загружаем файлы
                        foreach (FileRelease file in _startDir.ChildFiles)
                            if (!file.IsDirectory)
                                using (FileStream streamRead = new FileStream(file.DestPath, FileMode.Open, FileAccess.Read))
                                {
                                    this.ftpClient.PutFile(file.FileName, streamRead);
                                    this.OnUpLoadFileEvent(this, new UpLoadFileEventArgs(file));
                                }

                        //Рекурсивный вызов для каждой дочерней дирректории
                        foreach (FileRelease dir in _startDir.ChildFiles)
                            if (dir.IsDirectory)
                            {
                                Upload(dir, true);
                                //Выходим на шаг вверх
                                this.ftpClient.ChangeDirectoryUp();
                            }
                    };
                    Upload(this.RootRelease, false);
                }

                this.ftpClient.Dispose();
                this.OnEndUpLoadEvent(this, EventArgs.Empty);
            });

            return task;
        }
        /// <summary>
        /// Получить количесство файлов в релизе
        /// </summary>
        /// <param name="_start"></param>
        /// <returns></returns>
        public int GetNumberOfFiles(FileRelease _start)
        {
            int count = 1;
            foreach (FileRelease file in _start.ChildFiles)
            {
                if (file.IsDirectory)
                    count += GetNumberOfFiles(file);
                else
                    count++;
            }

            return count;
        }
    }

    /// <summary>
    /// Параметры события BeginUpLoadEvent
    /// </summary>
    public class BeginUpLoadEventArgs : EventArgs
    {
        /// <summary>
        /// Количество загружаемых файлов
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="_count"></param>
        public BeginUpLoadEventArgs(int _count)
        {
            this.Count = _count;
        }
    }
    /// <summary>
    /// Параметры события UpLoadFileEvent
    /// </summary>
    public class UpLoadFileEventArgs : EventArgs
    {
        /// <summary>
        /// Файл
        /// </summary>
        public FileRelease File { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="_file"></param>
        public UpLoadFileEventArgs(FileRelease _file)
        {
            this.File = _file;
        }
    }
}
