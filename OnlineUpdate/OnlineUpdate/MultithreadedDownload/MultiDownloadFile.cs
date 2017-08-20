using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Net;
using System.Collections.Concurrent;
using System.Threading;

namespace OnlineUpdate.MultithreadedDownload
{
    /// <summary>
    /// Класс реализует многопоточное скачивание файла
    /// </summary>
    public class MultiDownloadFile : IDisposable
    {
        /// <summary>
        /// Выполняется загрузка
        /// </summary>
        private bool isDownload;
        /// <summary>
        /// Список источников скачивания файла
        /// </summary>
        private List<Uri> sourceLinks;
        /// <summary>
        /// Поток записи временного файла
        /// </summary>
        private FileStream streamFileTemp;
        /// <summary>
        /// Счет загруженных байт
        /// </summary>
        private int countDownloadSize;
        /// <summary>
        /// Создание маркеров
        /// </summary>
        private CancellationTokenSource cancaleTokenSource = new CancellationTokenSource();

        /// <summary>
        /// Длина потока
        /// </summary>
        private int lengthStream;
        /// <summary>
        /// Коллекция скачиваемых областей
        /// </summary>
        private List<MultiDownloadFilePart> listParts = new List<MultiDownloadFilePart>();
        /// <summary>
        /// Очередь на запись во временный файл
        /// </summary>
        private ConcurrentQueue<DownloadFilePartReadEventArgs> orderForWriteToTempFile = new ConcurrentQueue<DownloadFilePartReadEventArgs>();

        private List<Task> listTasks = null;

        /// <summary>
        /// Имя файла
        /// </summary>
        public string NameFile { get; private set; }
        /// <summary>
        /// Путь к временному файлу
        /// </summary>
        public string PathFile { get; private set; }
        /// <summary>
        /// Длина потока
        /// </summary>
        public int LengthStream
        {
            get { return lengthStream; }
        }
        /// <summary>
        /// Хешь файла
        /// </summary>
        public string MD5 { get; private set; }
        /// <summary>
        /// Описание структрутры хранения во временном файле
        /// </summary>
        public HeaderTempFile HeaderFile { get; private set; }


        /// <summary>
        /// Обработчик начала загрузки файла
        /// </summary>
        public event EventHandler<StartDownloadEventArgs> StartDownloadEvent;
        /// <summary>
        /// Обработчик окончания загрузки
        /// </summary>
        public event EventHandler EndDownloadEvent;
        /// <summary>
        /// Событие записи буфера данных во временный файл
        /// </summary>
        public event EventHandler<SaveBuffEventArgs> SaveBuffEvent;


        /// <summary>
        /// Вызвать обработчик
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        public void RunStartDownloadEvent(object _sender, StartDownloadEventArgs _e)
        {
            var ev = this.StartDownloadEvent;
            if (ev != null)
                ev(_sender, _e);
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
                ev(_sender, _e);
        }
        /// <summary>
        /// Вызвать обработчик
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        public void RunSaveBuffEvent(object _sender, SaveBuffEventArgs _e)
        {
            var ev = this.SaveBuffEvent;
            if (ev != null)
                ev(_sender, _e);
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="_link"></param>
        /// <param name="_pathDest"></param>
        public MultiDownloadFile(Uri _link, string _pathDest)
            : this(new List<Uri>() { _link }, _pathDest, null)
        {

        }


        /// <summary>
        /// Конструктор класса
        /// </summary>
        public MultiDownloadFile(List<Uri> _sourceLinks, string _pathDest, string _md5)
        {
            this.sourceLinks = _sourceLinks;
            this.PathFile = _pathDest;
            this.NameFile = Path.GetFileName(_pathDest);
            int kolThreads = sourceLinks.Count;

            //Если временный файл существует пытаемся прочиать его заголовок
            if (File.Exists(_pathDest + MultiDownloadMenedger.ExtensioTempFiles))
            {
                this.HeaderFile = this.ReadHeader(_pathDest + MultiDownloadMenedger.ExtensioTempFiles);
                //Проверяем возможность использования этого временного файла
                if (this.HeaderFile != null && this.HeaderFile.MD5 == _md5 && kolThreads == this.HeaderFile.KolParts)
                {
                    //Получаем длину потока
                    lengthStream = (int)HTTPStream.GetLengthStream(sourceLinks[0]);

                    //Формируем список описания скачиваемых областей
                    for (int i = 0; i < kolThreads; i++)
                    {
                        if (this.HeaderFile.Parts[i].Length > this.HeaderFile.Parts[i].SizeDownloaded)
                        {
                            MultiDownloadFilePart thDownload = new MultiDownloadFilePart(sourceLinks[i], this.HeaderFile.Parts[i]);
                            thDownload.ReadEvent += thDownload_ReadEvent;
                            listParts.Add(thDownload);
                        }
                        countDownloadSize += this.HeaderFile.Parts[i].SizeDownloaded;
                    }
                    //Открываем временный файл для догрузки
                    this.OpenTempFile();
                }
                else
                {
                    //Если файл не подходит для докачивания удаляем его
                    this.HeaderFile = null;
                    File.Delete(_pathDest + MultiDownloadMenedger.ExtensioTempFiles);
                }
            }

            //Если это загрузка файла с нуля, создаем опиание заголовка
            if (this.HeaderFile == null)
            {
                this.HeaderFile = new HeaderTempFile()
                {
                    KolParts = kolThreads,
                    MD5 = _md5,
                    Parts = new List<HeaderPartTempFile>()
                };

                //Получаем длину потока
                lengthStream = (int)HTTPStream.GetLengthStream(sourceLinks[0]);

                //Формируем список описания скачиваемых областей
                for (int i = 0; i < kolThreads; i++)
                {
                    int lengthPart = this.GetLengthPart(lengthStream, kolThreads);
                    //Последняя област может быть немного больше
                    int addLastPart = lengthStream - lengthPart * kolThreads;
                    //Стратовая позиция чтения в HTTP потоке 
                    int startPosHttpStream = i * lengthPart;
                    //Стратовая позиция записи области в файле
                    int startPosFileStream = MultiDownloadMenedger.StartFirstPart + (lengthPart * i);

                    MultiDownloadFilePart thDownload = new MultiDownloadFilePart(sourceLinks[i], startPosHttpStream, startPosFileStream, lengthPart + (i == kolThreads - 1 ? addLastPart : 0));
                    thDownload.ReadEvent += thDownload_ReadEvent;
                    listParts.Add(thDownload);

                    this.HeaderFile.Parts.Add(thDownload.HeaderPart);
                }
                //Создаем временный файл
                this.CreateTempFile();
            }
        }

        /// <summary>
        /// Освобождение ресурсов
        /// </summary>
        public void Dispose()
        {
            if (this.StartDownloadEvent != null)
                foreach (EventHandler<StartDownloadEventArgs> ev in this.StartDownloadEvent.GetInvocationList())
                    StartDownloadEvent -= ev;

            if (this.EndDownloadEvent != null)
                foreach (EventHandler ev in this.EndDownloadEvent.GetInvocationList())
                    EndDownloadEvent -= ev;

            if (this.SaveBuffEvent != null)
                foreach (EventHandler<SaveBuffEventArgs> ev in this.SaveBuffEvent.GetInvocationList())
                    SaveBuffEvent -= ev;

            foreach (MultiDownloadFilePart part in this.listParts)
                part.Dispose();
        }

        #region Handle part events

        /// <summary>
        /// Обработчик считывания очередной части области из потока HTTP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void thDownload_ReadEvent(object sender, DownloadFilePartReadEventArgs e)
        {
            orderForWriteToTempFile.Enqueue(e);
        }

        #endregion

        /// <summary>
        /// Начать загрузку файла
        /// </summary>
        public Task DownloadAsync()
        {
            CancellationToken cancaleToken = cancaleTokenSource.Token;
            this.isDownload = true;
            //Запускаем обработку очереди записи
            Task taskOrderWrite = this.StartHendlerOrderWriteDataAsync(cancaleToken);

            Task task = Task.Factory.StartNew(() =>
            {
                try
                {
                    //Вызвать событие
                    this.RunStartDownloadEvent(this, new StartDownloadEventArgs(this.NameFile, this.lengthStream, countDownloadSize, listParts.Count));

                    listTasks = new List<Task>();
                    //Запускаем потоки чтения
                    foreach (MultiDownloadFilePart part in listParts)
                        listTasks.Add(part.DownloadAsync(cancaleToken));

                    try
                    {
                        //Ждем завершения потоков чтения
                        Task.WaitAll(listTasks.ToArray());
                    }
                    catch (OperationCanceledException)
                    {

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    //Балансировка времени задержки
                    if (this.LengthStream <= 716800)
                        System.Threading.Thread.Sleep(1000);
                    else
                        System.Threading.Thread.Sleep(200);

                    //Отмена выполнения
                    if (cancaleToken.IsCancellationRequested)
                        cancaleToken.ThrowIfCancellationRequested();

                    this.isDownload = false;

                    //Ожидаем завершения потока обработки очереди записи
                    taskOrderWrite.Wait();

                    //Удаляем заголовок описания
                    this.RemoveHeader();
                }
                catch (OperationCanceledException)
                {

                }
                catch (Exception ex)
                {
                    if (!(ex.InnerException is OperationCanceledException))
                        throw new Exception("Error download file", ex);
                }
                finally
                {
                    if (streamFileTemp != null)
                        try
                        {
                            streamFileTemp.Dispose();
                        }
                        catch { }
                }
            }, cancaleToken);

            return task;
        }
        /// <summary>
        /// Отменить выполнение
        /// </summary>
        public void CancelDownload()
        {
            this.cancaleTokenSource.Cancel();
        }

        #region Private methods

        /// <summary>
        /// Запустить обработчик очереди записи загруженных данных
        /// </summary>
        private Task StartHendlerOrderWriteDataAsync(CancellationToken _cancaleToken)
        {
            Task task = Task.Factory.StartNew(() =>
            {
                try
                {
                    while (this.isDownload)
                    {
                        DownloadFilePartReadEventArgs data = null;
                        while (this.orderForWriteToTempFile.TryDequeue(out data))
                        {
                            //Записать данные во временный файл
                            int position = data.Sender.HeaderPart.StartPositionFileStream + data.Sender.HeaderPart.SizeDownloaded;
                            this.streamFileTemp.Position = position;
                            this.streamFileTemp.Write(data.BuffRead, 0, data.ReadLength);

                            data.Sender.HeaderPart.SizeDownloaded += data.ReadLength;

                            //Перезапись заголовка
                            WriteHeader();

                            countDownloadSize += data.ReadLength;
                            //Вызвать обработчик
                            this.RunSaveBuffEvent(this, new SaveBuffEventArgs(data.ReadLength));

                            //Отмена выполнения
                            if (_cancaleToken.IsCancellationRequested)
                                _cancaleToken.ThrowIfCancellationRequested();
                        }

                        System.Threading.Thread.Sleep(10);

                        //Отмена выполнения
                        if (_cancaleToken.IsCancellationRequested)
                            _cancaleToken.ThrowIfCancellationRequested();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }, _cancaleToken);

            return task;
        }
        /// <summary>
        /// Получить длину области во временном файле
        /// </summary>
        /// <param name="_sourceLength"></param>
        /// <param name="_kolThread"></param>
        /// <returns></returns>
        private int GetLengthPart(int _sourceLength, int _kolThread)
        {
            for (int i = 0; i < 1000; i++)
            {
                if ((_sourceLength - i) % _kolThread == 0)
                    return (_sourceLength - i) / _kolThread;
            }

            throw new Exception("Unable to calculate the size of the download area");
        }
        /// <summary>
        /// Создать временный файл
        /// </summary>
        /// <param name="_pathFile"></param>
        /// <param name="_setLengthStream"></param>
        private void CreateTempFile()
        {
            try
            {
                streamFileTemp = File.Create(this.PathFile + MultiDownloadMenedger.ExtensioTempFiles);
                streamFileTemp.SetLength(this.lengthStream + MultiDownloadMenedger.SizeHeaderRange);

                this.WriteHeader();

                streamFileTemp.Flush();
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to create a temporary file to download", ex);
            }
        }
        /// <summary>
        /// Открыть временный файл для докачивания
        /// </summary>
        private void OpenTempFile()
        {
            try
            {
                streamFileTemp = new FileStream(this.PathFile + MultiDownloadMenedger.ExtensioTempFiles, FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to open a temporary file to download", ex);
            }
        }
        /// <summary>
        /// Записть заголовок временного файла
        /// </summary>
        private void WriteHeader()
        {
            try
            {
                string strHeader = this.HeaderFile.Serilization();
                byte[] buffWrite = Encoding.UTF8.GetBytes(strHeader);
                streamFileTemp.Position = this.lengthStream + 1;
                streamFileTemp.Write(buffWrite, 0, buffWrite.Length);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to create a temporary file to download", ex);
            }
        }
        /// <summary>
        /// Удалить из файоа заголовок описания структруры скачивания
        /// </summary>
        private void RemoveHeader()
        {
            try
            {
                streamFileTemp.SetLength(this.lengthStream);
                streamFileTemp.Close();
                streamFileTemp.Dispose();

                if (File.Exists(this.PathFile))
                    File.Delete(this.PathFile);

                //Переименовываем файл
                File.Move(this.PathFile + MultiDownloadMenedger.ExtensioTempFiles, this.PathFile);

                //Вызвать обработчик завершения
                this.RunEndDownloadEvent(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                throw new Exception("Failure to remove the title from the temporary file", ex);
            }
        }
        /// <summary>
        /// Прочитать заголовк файла
        /// </summary>
        /// <param name="_pathFile"></param>
        /// <returns></returns>
        private HeaderTempFile ReadHeader(string _pathFile)
        {
            try
            {
                using (FileStream stream = new FileStream(_pathFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    stream.Position = stream.Length - MultiDownloadMenedger.SizeHeaderRange + 1;
                    byte[] buff = new byte[MultiDownloadMenedger.SizeHeaderRange];
                    stream.Read(buff, 0, MultiDownloadMenedger.SizeHeaderRange);

                    int countByte = 0;
                    for (int i = 0; i < buff.Length; i++)
                    {
                        if (buff[i] == 0)
                        {
                            countByte = i - 1;
                            break;
                        }
                    }

                    using (MemoryStream streamTemp = new MemoryStream(buff, 0, countByte))
                    {
                        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(HeaderTempFile));
                        HeaderTempFile resp = (HeaderTempFile)ser.ReadObject(streamTemp);

                        return resp;
                    }
                }
            }
            catch
            {

            }

            return null;
        }

        #endregion
    }
}
