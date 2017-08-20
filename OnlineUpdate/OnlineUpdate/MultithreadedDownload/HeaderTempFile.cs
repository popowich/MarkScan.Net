using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;

namespace OnlineUpdate.MultithreadedDownload
{
    /// <summary>
    /// Класс реализует JSON обертку заголовка временного файла
    /// </summary>
    [DataContract]
    public class HeaderTempFile
    {
        /// <summary>
        /// Хешь файла
        /// </summary>
        [DataMember(Name = "MD5")]
        public string MD5 { get; set; }
        /// <summary>
        /// Количество разделов
        /// </summary>
        [DataMember(Name = "KolParts")]
        public int KolParts { get; set; }
        /// <summary>
        /// Описание разделов
        /// </summary>
        [DataMember(Name = "Parts")]
        public List<HeaderPartTempFile> Parts { get; set; }

        /// <summary>
        /// Серилизовать объект
        /// </summary>
        /// <returns></returns>
        public string Serilization()
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(this.GetType());
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, this);

            return Encoding.Default.GetString(ms.ToArray());
        }
    }
    /// <summary>
    /// Класс реализует JSON обертку описания области временного файла
    /// </summary>
    [DataContract]
    public class HeaderPartTempFile
    {
        /// <summary>
        /// Стратовая позиция области в потоке чтения
        /// </summary>
        [DataMember(Name = "StartPositionHttpStream")]
        public int StartPositionHttpStream { get; set; }
        /// <summary>
        /// Стратовая позиция области в файле
        /// </summary>
        [DataMember(Name = "StartPositionFileStream")]
        public int StartPositionFileStream { get; set; }
        /// <summary>
        /// Длинна области
        /// </summary>
        [DataMember(Name = "Length")]
        public int Length { get; set; }
        /// <summary>
        /// Размер загруженной области
        /// </summary>
        [DataMember(Name = "SizeDownloaded")]
        public int SizeDownloaded { get; set; }
    }
}
