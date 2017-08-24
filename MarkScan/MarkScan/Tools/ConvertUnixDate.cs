using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkScan.Tools
{
    internal static class ConvertUnixDate
    {
        /// <summary>
        /// Конвертировать дата/время из Unix формата
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        internal static DateTime ConvertFromUnixTimestamp(string timestamp)
        {
            double tt = Double.Parse(timestamp);
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(tt);
        }
        /// <summary>
        /// Конвертировать дата/время в Unix формат
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        internal static uint ConvertInUnixTimestamp(DateTime _datetime)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan elapsedSpan = new TimeSpan(_datetime.Ticks - origin.Ticks);

            return (uint)elapsedSpan.TotalSeconds;
        }
    }
}
