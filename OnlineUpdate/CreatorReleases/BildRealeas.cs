using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ICSharpCode.SharpZipLib.BZip2;

namespace CreatorReleases
{
    /// <summary>
    /// Построитель релизов обновления
    /// </summary>
    public class BildRealeas : ITaskExecution, IDisposable
    {
        /// <summary>
        /// Имя файла описания релиза
        /// </summary>
        private readonly string fileNameDescriptionReleas = "UpdateDescription.xml";

        /// <summary>
        /// Версия релиза
        /// </summary>
        private string versionReleas;
        /// <summary>
        /// Дата публикации
        /// </summary>
        private DateTime dateBild;
        /// <summary>
        /// Текстовое описание обновления
        /// </summary>
        private string updateDescriptiones;
        /// <summary>
        /// Корневой каталог размещения релиза
        /// </summary>
        private string destRootDir;
        /// <summary>
        /// Корневой каталог источник
        /// </summary>
        private string sourceRootDir;
        /// <summary>
        /// Сжимать файлы релиза
        /// </summary>
        public bool IsCompressFiles { get; set; }
        /// <summary>
        /// Корень релиза
        /// </summary>
        public FileRelease RootRelease { get; set; }
        /// <summary>
        /// Релиз создан успешно
        /// </summary>
        public bool IsCreatedSuccessfully { get; private set; }

        #region Eventes

        /// <summary>
        /// Событие старта создания релиза
        /// </summary>
        public event EventHandler<BeginBildEventArgs> BeginBildReleaseEvent;
        /// <summary>
        /// Событие создания файла релиза
        /// </summary>
        public event EventHandler<CreatedFileEventArgs> CreatedFileEvent;
        /// <summary>
        /// Событие окончания создания релиза
        /// </summary>
        public event EventHandler EndBildReleaseEvent;
        /// <summary>
        /// Событие ошибки
        /// </summary>
        public event ErrorEventHandler ErrorEvent;

        /// <summary>
        /// Вызвать обработку события
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        public void OnBeginBildReleaseEvent(object _sender, BeginBildEventArgs _e)
        {
            var evMethod = this.BeginBildReleaseEvent;
            if (evMethod != null)
                evMethod(_sender, _e);
        }
        /// <summary>
        /// Вызвать обработку события
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        public void OnCreatedFileEvent(object _sender, CreatedFileEventArgs _e)
        {
            var evMethod = this.CreatedFileEvent;
            if (evMethod != null)
                evMethod(_sender, _e);
        }
        /// <summary>
        /// Вызвать обработку события
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        public void OnEndBildReleaseEvent(object _sender, EventArgs _e)
        {
            var evMethod = this.EndBildReleaseEvent;
            if (evMethod != null)
                evMethod(_sender, _e);
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
        /// Конструктор класса
        /// </summary>
        public BildRealeas(string _sourceDirr, bool _compressFiles, string _currVersion, DateTime _dateBild, string _descriptiones)
        {
            this.destRootDir = GetCurrDirrForRealeas();
            this.sourceRootDir = _sourceDirr;
            this.IsCompressFiles = _compressFiles;
            this.versionReleas = _currVersion;
            this.dateBild = _dateBild;
            this.updateDescriptiones = _descriptiones;
        }
        /// <summary>
        /// Освободить ресурсы
        /// </summary>
        public void Dispose()
        {
            if (this.CreatedFileEvent != null)
                foreach (EventHandler<CreatedFileEventArgs> ev in this.CreatedFileEvent.GetInvocationList())
                    this.CreatedFileEvent -= ev;

            if (this.EndBildReleaseEvent != null)
                foreach (EventHandler ev in this.EndBildReleaseEvent.GetInvocationList())
                    this.EndBildReleaseEvent -= ev;

            if (this.BeginBildReleaseEvent != null)
                foreach (EventHandler<BeginBildEventArgs> ev in this.BeginBildReleaseEvent.GetInvocationList())
                    this.BeginBildReleaseEvent -= ev;

            if (this.ErrorEvent != null)
                foreach (ErrorEventHandler ev in this.ErrorEvent.GetInvocationList())
                    this.ErrorEvent -= ev;
        }

        /// <summary>
        /// Построить дерево релиза
        /// </summary>
        public void CreateTreeRelease()
        {
            this.IsCreatedSuccessfully = false;
            RootRelease = new FileRelease(true, sourceRootDir, null);
            Action<FileRelease> CreateTreeReleaseRecursion = null;

            CreateTreeReleaseRecursion = (FileRelease _rootDirr) =>
            {
                //Заполняем коллекцию дочерними директориями
                string[] directoryes = Directory.GetDirectories(_rootDirr.SourcePath);
                foreach (string dir in directoryes)
                {
                    _rootDirr.ChildFiles.Add(new FileRelease(true, dir, _rootDirr));
                    CreateTreeReleaseRecursion(_rootDirr.ChildFiles[_rootDirr.ChildFiles.Count - 1]);
                }
                //Заполняем коллекцию дочерних файлов
                string[] files = Directory.GetFiles(_rootDirr.SourcePath);

                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file);
                    PermitsPattern permits = AppSettings.Settings.PermitsPatterns.Find(pp => pp.Compea(fileName));
                    IgnorePattern ignor = null;

                    if (permits == null)
                        ignor = AppSettings.Settings.IgnorePatterns.Find(ig => ig.Compea(fileName));

                    if (ignor == null)
                        _rootDirr.ChildFiles.Add(new FileRelease(false, file, _rootDirr));
                }
            };

            CreateTreeReleaseRecursion(this.RootRelease);
        }
        /// <summary>
        /// Построить релиз
        /// </summary>
        public Task Begin()
        {
            this.IsCreatedSuccessfully = false;
            Task task = Task.Factory.StartNew(() =>
            {
                this.OnBeginBildReleaseEvent(this, new BeginBildEventArgs(GetNumberOfFiles(this.RootRelease)));

                try
                {
                    //Создание корневой дирректории
                    if (Directory.Exists(this.destRootDir))
                        Directory.Delete(this.destRootDir, true);

                    //Рекурсивное создание релиза
                    Action<FileRelease> BildFilesRecursion = null;
                    BildFilesRecursion = (FileRelease _rootDirr) =>
                    {
                        Directory.CreateDirectory(_rootDirr.DestPath);
                        foreach (FileRelease file in _rootDirr.ChildFiles)
                        {
                            //Если это дирректория выполняем рек. вызов
                            if (file.IsDirectory)
                            {
                                BildFilesRecursion(file);
                            }
                            else
                            {
                                //Копирование и сжатие файла 
                                file.CopyFileFromSource(IsCompressFiles);

                                this.OnCreatedFileEvent(this, new CreatedFileEventArgs(file));
                            }
                        }
                    };

                    BildFilesRecursion(this.RootRelease);

                    //Создать файл описания релиза
                    this.WriteFileDescription();
                  
                    this.IsCreatedSuccessfully = true;
                }
                catch (Exception ex)
                {
                    this.OnErrorEvent(this, new ErrorEventArgs(ex));
                }

                this.OnEndBildReleaseEvent(this, EventArgs.Empty);
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
            int count = 0;
            foreach (FileRelease file in _start.ChildFiles)
            {
                if (file.IsDirectory)
                    count += GetNumberOfFiles(file);
                else
                    count++;
            }

            return count;
        }

        /// <summary>
        /// Записать файл описания
        /// </summary>
        private void WriteFileDescription()
        {
            try
            {
                System.Xml.XmlDocument document = new System.Xml.XmlDocument();
                document.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><head></head>");

                System.Xml.XmlNode element = document.CreateElement("UpgradeToVersion");
                element.InnerText = this.versionReleas;
                document.DocumentElement.AppendChild(element);

                element = document.CreateElement("PublicationDate");
                element.InnerText = this.dateBild.ToShortDateString();
                document.DocumentElement.AppendChild(element);

                element = document.CreateElement("UpdateDescriptiones");
                element.InnerText = this.updateDescriptiones;
                document.DocumentElement.AppendChild(element);

                element = document.CreateElement("UpdateFiles");
                document.DocumentElement.AppendChild(element);

                //Рекурсивное создание списка содержимого
                Action<FileRelease> AddContentXml = null;
                AddContentXml = (FileRelease _file) =>
                {
                    //Добавляем файлы
                    foreach (FileRelease file in _file.ChildFiles)
                        if (!file.IsDirectory)
                        {
                            System.Xml.XmlNode elementCild = document.CreateElement("File");
                            elementCild.Attributes.Append(document.CreateAttribute("PathInstall")).InnerText = file.PathInstall;
                            elementCild.Attributes.Append(document.CreateAttribute("PathDownload")).InnerText = file.PathDownload;
                            elementCild.Attributes.Append(document.CreateAttribute("MD5")).InnerText = file.MD5;
                            elementCild.Attributes.Append(document.CreateAttribute("Size")).InnerText = file.SizeKb.ToString();
                            element.AppendChild(elementCild);
                        }

                    //Добавляем описание дирректории
                    foreach (FileRelease dir in _file.ChildFiles)
                        if (dir.IsDirectory)
                        {
                            AddContentXml(dir);
                        }
                };

                AddContentXml(this.RootRelease);
                //Сохраняем xml файл
                document.Save(this.destRootDir + "\\" + fileNameDescriptionReleas);

                FileRelease fileDescript = new FileRelease(false, this.destRootDir + "\\" + fileNameDescriptionReleas, this.destRootDir + "\\" + fileNameDescriptionReleas, this.RootRelease);                     
                //Сжатие файл 
                if (this.IsCompressFiles)
                {
                    FileInfo fileToBeZipped = new FileInfo(this.destRootDir + "\\" + fileNameDescriptionReleas);
                    FileInfo zipFileName = new FileInfo(string.Concat(this.destRootDir + "\\" + fileNameDescriptionReleas, ".bz2"));
                    using (FileStream fileToBeZippedAsStream = fileToBeZipped.OpenRead())
                    {
                        using (FileStream zipTargetAsStream = zipFileName.Create())
                        {
                            BZip2.Compress(fileToBeZippedAsStream, zipTargetAsStream, 4096);
                        }
                    }
                    fileDescript.DestPath = fileDescript.DestPath + ".bz2";
                    fileDescript.FileNameOriginal = fileDescript.FileNameOriginal + ".bz2";
                    fileDescript.FileName = fileDescript.FileNameOriginal;
                }

                //Добавляем файл в дерево релиза
                this.RootRelease.ChildFiles.Add(fileDescript);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating file descriptions release", ex);
            }
        }

        #region Static Methods

        /// <summary>
        /// Получить рассположение корневого каталога создания релиза
        /// </summary>
        /// <returns></returns>
        public static string GetCurrDirrForRealeas()
        {
            return AppSettings.Settings.BildReleasesDir + "\\" + AppSettings.Settings.NameCreateDirReleas + " " + AppSettings.Settings.CurrVersione;
        }

        #endregion
    }

    /// <summary>
    /// Переопределение класса EventArgs
    /// </summary>
    public class CreatedFileEventArgs : EventArgs
    {
        /// <summary>
        /// Файл
        /// </summary>
        public FileRelease File { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="_file"></param>
        public CreatedFileEventArgs(FileRelease _file)
        {
            this.File = _file;
        }
    }
    /// <summary>
    /// Переопределение класса EventArgs
    /// </summary>
    public class BeginBildEventArgs : EventArgs
    {
        /// <summary>
        /// Количесвто файлов
        /// </summary>
        public int CountFiles { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="_file"></param>
        public BeginBildEventArgs(int _count)
        {
            this.CountFiles = _count;
        }
    }
}
