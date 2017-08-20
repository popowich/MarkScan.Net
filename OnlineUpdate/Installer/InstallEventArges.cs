using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Installer
{
    public class InstallFileEventArgs : EventArgs
    {
        /// <summary>
        /// Устанавливаемый файл
        /// </summary>
        public FileInstall File { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="_file"></param>
        public InstallFileEventArgs(FileInstall _file)
        {
            this.File = _file;
        }
    }

    public class BeginInstallEventArgs : EventArgs
    {
        /// <summary>
        /// Устанавливаемый файл
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="_file"></param>
        public BeginInstallEventArgs(int _cout)
        {
            this.Count = _cout;
        }
    }

    public class ErrorEventArgs : EventArgs
    {
        public Exception exept;
        public string descriptione;

        public ErrorEventArgs(Exception _exept)
        {
            exept = _exept;
        }
   
    }

    public class ChangingPhaseOperationEventArgs:EventArgs
    {
        /// <summary>
        /// Наименвоание этапа
        /// </summary>
        public string NamePhase { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="_namePhase"></param>
        public ChangingPhaseOperationEventArgs(string _namePhase)
        {
            this.NamePhase = _namePhase;
        }
    }
}
