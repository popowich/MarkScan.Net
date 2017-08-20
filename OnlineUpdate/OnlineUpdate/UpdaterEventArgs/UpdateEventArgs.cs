using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineUpdate.UpdaterEventArgs
{
    public class EndChekUpdateEventArgs : EventArgs
    {
       public UpdateDescription Description { get; set; }
    }

    public class LoadedFileUpdateEventArgs : EventArgs
    {
        /// <summary>
        /// Файл
        /// </summary>
        public FileUpdate File { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="_file"></param>
        public LoadedFileUpdateEventArgs(FileUpdate _file)
        {
            this.File = _file;
        }
    }

    public class EndLoadUpdateFilesEventArgs : EventArgs
    {
        public UpdateDescription Description { get; set; }

        /// <summary>
        /// Список файлов для обновления
        /// </summary>
        public List<FileUpdate> FilesUpdate { get; private set; }
        /// <summary>
        /// Отменить продолжение операции
        /// </summary>
        public bool Cancel { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="_file"></param>
        public EndLoadUpdateFilesEventArgs(UpdateDescription desc, List<FileUpdate> _filesUpdate, bool _cancel)
        {
            Description = desc;
            this.FilesUpdate = _filesUpdate;
            this.Cancel = _cancel;
        }
    }

    public class BeforeInstallingEventArgs : EventArgs
    {
        /// <summary>
        /// Список файлов для обновления
        /// </summary>
        public List<FileUpdate> FilesUpdate { get; private set; }
        /// <summary>
        /// Отменить продолжение операции
        /// </summary>
        public bool Cancel { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="_file"></param>
        public BeforeInstallingEventArgs(List<FileUpdate> _filesUpdate, bool _cancel)
        {
            this.FilesUpdate = _filesUpdate;
            this.Cancel = _cancel;
        }
    }
    /// <summary>
    /// Класс контейнер описания исключительной ситуации во время обновления
    /// </summary>
    public class UpdateErrorEventArgs : EventArgs
    {
        public UpdateErrorEventArgs(Exception _exept, string _descriptione)
        {
            exept = _exept;
            descriptione = _descriptione;
        }
        public Exception exept;
        public string descriptione;
    }
}
