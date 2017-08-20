using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlineUpdate;

namespace OnlineUpdate.FileInstaller
{
    public class InstallFileEventArgs : EventArgs
    {
        /// <summary>
        /// Устанавливаемый файл
        /// </summary>
        public FileUpdate File { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="_file"></param>
        public InstallFileEventArgs(FileUpdate _file)
        {
            this.File = _file;
        }
    }

    public class BeginPreparingInstallationEventArgs : EventArgs
    {
        /// <summary>
        /// Устанавливаемый файл
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="_file"></param>
        public BeginPreparingInstallationEventArgs(int _cout)
        {
            this.Count = _cout;
        }
    }

    public class EndPreparingInstallationEventArgs : EventArgs
    {
        /// <summary>
        /// Выполнено успешно
        /// </summary>
        public bool Succeeded { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public EndPreparingInstallationEventArgs(bool _succeeded)
        {
            this.Succeeded = _succeeded;
        }
    }
}
