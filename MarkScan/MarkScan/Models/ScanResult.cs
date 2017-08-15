using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkScan.Models
{
    public class ScanResult
    {
        /// <summary>
        /// Акцизная марка
        /// </summary>
        internal string ExciseStamp { get; set; }
        /// <summary>
        /// Алкод
        /// </summary>
        internal string AlcCode { get; set; }
    }
}
