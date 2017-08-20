using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OnlineUpdate.MultithreadedDownload
{
    /// <summary>
    /// Менеджер подсистемы многопоточной загрузки файлов
    /// </summary>
    public static class MultiDownloadMenedger
    {
        /// <summary>
        /// Лоадер
        /// </summary>
        private static MultiDownloadFile multiLoader = null;
        /// <summary>
        /// Отмена операции
        /// </summary>
        private static bool isCancle;

        /// <summary>
        /// Расширение временного файла
        /// </summary>
        public static readonly string ExtensioTempFiles = ".download";
        /// <summary>
        /// Размер заголовочной области
        /// </summary>
        public static readonly int SizeHeaderRange = 10024;
        /// <summary>
        /// Начало первой области записи во временном файле
        /// </summary>
        public static readonly int StartFirstPart = 0;

        /// <summary>
        /// Источники скачивания
        /// </summary>
        public static List<Uri> SourceDownloads { get; set; }

        #region Static eventes

        /// <summary>
        /// Событие начала загрузки файла
        /// </summary>
        public static event EventHandler<StartDownloadEventArgs> StartDownloadFileEvent;
        /// <summary>
        /// Событие заврешения загрузки файла
        /// </summary>
        public static event EventHandler EndDownloadFileEvent;
        /// <summary>
        /// Событие обработчик загрузки блока данных
        /// </summary>
        public static event EventHandler<SaveBuffEventArgs> ReadRangeEvent;

        /// <summary>
        /// Вызвать обработчик
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private static void OnStartDownloadFileEvent(object _sender, StartDownloadEventArgs _e)
        {
            var ev = StartDownloadFileEvent;
            if (ev != null)
                ev(_sender, _e);
        }
        /// <summary>
        /// Вызвать обработчик
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private static void OnEndDownloadFileEvent(object _sender, EventArgs _e)
        {
            var ev = EndDownloadFileEvent;
            if (ev != null)
                ev(_sender, _e);
        }
        /// <summary>
        /// Вызвать обработчик
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private static void OnReadRangeEvent(object _sender, SaveBuffEventArgs _e)
        {
            var ev = ReadRangeEvent;
            if (ev != null)
                ev(_sender, _e);
        }

        #endregion

        #region Internal handlers

        private static void loader_EndDownloadEvent(object sender, EventArgs e)
        {
            OnEndDownloadFileEvent(sender, e);
        }

        private static void loader_StartDownloadEvent(object sender, StartDownloadEventArgs e)
        {
            OnStartDownloadFileEvent(sender, e);
        }

        private static void loader_SaveBuffEvent(object sender, SaveBuffEventArgs e)
        {
            OnReadRangeEvent(sender, e);
        }

        #endregion

        /// <summary>
        /// Инициализация подсистемы
        /// </summary>
        public static void InitMenedger(List<Uri> _linlks)
        {
            SourceDownloads = _linlks;

            ServicePointManager.DefaultConnectionLimit = 1000;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.UseNagleAlgorithm = false;
        }
        /// <summary>
        /// Освободить ресурсы
        /// </summary>
        public static void Dispose()
        {
            if (StartDownloadFileEvent != null)
                foreach (EventHandler<StartDownloadEventArgs> ev in StartDownloadFileEvent.GetInvocationList())
                    StartDownloadFileEvent -= ev;

            if (EndDownloadFileEvent != null)
                foreach (EventHandler ev in EndDownloadFileEvent.GetInvocationList())
                    EndDownloadFileEvent -= ev;

            if (ReadRangeEvent != null)
                foreach (EventHandler<SaveBuffEventArgs> ev in ReadRangeEvent.GetInvocationList())
                    ReadRangeEvent -= ev;
        }

        /// <summary>
        /// Загрузить файл синхронно
        /// </summary>
        /// <param name="_link"></param>
        /// <param name="_pathDesp"></param>
        /// <returns></returns>
        public static bool DownloadFile(string _link, string _pathDesp)
        {
            //У нас есть 5-ть попыток скачать этот файл
            int countTry = 0;
            while (countTry <= 5)
            {
                MultiDownloadFile loader = null;
                try
                {
                    loader = new MultiDownloadFile(new Uri(_link), _pathDesp);
                    Task task = loader.DownloadAsync();
                    task.Wait();

                    return true;
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    if (countTry == 5)
                        throw new Exception("Expired number of attempts to download the file", ex);
                    else
                    {
                        System.Threading.Thread.Sleep(3000);
                        countTry++;
                    }
                }
                finally
                {
                    if (loader != null)
                        loader.Dispose();
                }
            }

            return false;
        }
        /// <summary>
        /// Загрузить файл в многопоточном режиме
        /// </summary>
        /// <param name="_relativelLink"></param>
        /// <param name="_pathDesp"></param>
        /// <param name="_md5"></param>
        /// <returns></returns>
        public static bool MultiDownloadFile(string _relativelLink, string _pathDesp, string _md5)
        {
            isCancle = false;
            //У нас есть 3и попыток скачать этот файл
            int countTry = 0;
            while (countTry <= 2 && isCancle == false)
            {
                try
                {
                    //Балансировка потоков
                    Uri mainLink = new Uri(SourceDownloads[0], _relativelLink);
                    long sizeFile = HTTPStream.GetLengthStream(mainLink);

                    int KolThread = 0;
                    if (sizeFile <= 1048576)
                        KolThread = 1;
                    else if (sizeFile <= 10485760)
                        KolThread = 2;
                    else if (sizeFile <= 104857600)
                        KolThread = 10;
                    else
                        KolThread = 20;

                    List<Uri> links = new List<Uri>();
                    for (int i = 1; i <= KolThread && links.Count < KolThread; i++)
                        foreach (Uri source in SourceDownloads)
                            links.Add(new Uri(source, _relativelLink));

                    multiLoader = new MultiDownloadFile(links, _pathDesp, _md5);
                    multiLoader.SaveBuffEvent += loader_SaveBuffEvent;
                    multiLoader.StartDownloadEvent += loader_StartDownloadEvent;
                    multiLoader.EndDownloadEvent += loader_EndDownloadEvent;

                    Task task = multiLoader.DownloadAsync();
                    task.Wait();

                    return true;
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    if (!isCancle)
                    {
                        if (countTry == 2)
                            throw new Exception("Expired number of attempts to download the file", ex);
                        else
                        {
                            System.Threading.Thread.Sleep(3000);
                            countTry++;
                        }
                    }
                }
                finally
                {
                    if (multiLoader != null)
                        multiLoader.Dispose();
                    multiLoader = null;
                }
            }

            return false;
        }
        /// <summary>
        /// Отменить загрузку файла
        /// </summary>
        public static void CancelDownload()
        {
            isCancle = true;
            if (multiLoader != null)
                multiLoader.CancelDownload();
        }
    }
}
