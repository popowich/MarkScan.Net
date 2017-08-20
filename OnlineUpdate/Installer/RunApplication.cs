using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Installer
{
    /// <summary>
    /// Описываем завершаемое приложение
    /// </summary>
    public class RunApplication
    {
        /// <summary>
        /// Путь к запускаемому приложения
        /// </summary>
        public string LocalPath { get; set; }
        /// <summary>
        /// Имя сервиса
        /// </summary>
        public string NameService { get; set; }
        /// <summary>
        /// Это сервис
        /// </summary>
        public bool IsService { get; set; }
        /// <summary>
        /// Аргументы запуска
        /// </summary>
        public string Argumentes { get; set; }
        /// <summary>
        /// Запустить после установки
        /// </summary>
        public bool IsRun { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public RunApplication(string _localPath, string _argumentes, string _nameService, bool _isService)
        {
            this.LocalPath = _localPath;
            this.IsService = _isService;
            this.NameService = _nameService;
            this.Argumentes = _argumentes;
        }
    }
}
