using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineUpdate.UpdaterEventArgs
{
    /// <summary>
    /// Реализация обертки исключения проверки налмчмя обновлений
    /// </summary>
    public class UpdateCheckingException : Exception
    {
        public UpdateCheckingException()
            : base()
        {

        }

        public UpdateCheckingException(string _text)
            : base(_text)
        {

        }

        public UpdateCheckingException(string _text, Exception _ex)
            : base(_text, _ex)
        {

        }
    }
    /// <summary>
    /// Реализация обертки исключения загрузки файлов
    /// </summary>
    public class UpdateDownloadException : Exception
    {
        public UpdateDownloadException()
            : base()
        {

        }

        public UpdateDownloadException(string _text)
            : base(_text)
        {

        }

        public UpdateDownloadException(string _text, Exception _ex)
            : base(_text, _ex)
        {

        }
    }
    /// <summary>
    /// Реализация обертки исключения устанвоки файлов
    /// </summary>
    public class UpdateInstallException : Exception
    {
        public UpdateInstallException()
            : base()
        {

        }

        public UpdateInstallException(string _text)
            : base(_text)
        {

        }

        public UpdateInstallException(string _text, Exception _ex)
            : base(_text, _ex)
        {

        }
    }
}
