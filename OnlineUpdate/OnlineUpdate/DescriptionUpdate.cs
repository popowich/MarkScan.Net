using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineUpdate
{
    public class UpdateDescription
    {
        /// <summary>
        /// Обновление разрешено
        /// </summary>
        public bool AllowUpdate { get; set; }
        /// <summary>
        /// Обновление будет выполено до версии
        /// </summary>
        public Version UpgradeToVersion { get; set; }
        /// <summary>
        /// Дата публикации обновления
        /// </summary>
        public DateTime PublicationDate { get; set; }
        /// <summary>
        /// Текстовое описание обноваления
        /// </summary>
        public string UpdateDescriptiones { get; set; }
        /// <summary>
        /// Количество обновляемых файлов
        /// </summary>
        public int CountFiles { get; set; }
    }
}
