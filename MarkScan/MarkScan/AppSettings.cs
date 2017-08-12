using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkScan
{
    internal static class AppSettings
    {
        internal static string NameAssembly
        {
            get
            {
                return System.Reflection.Assembly.GetEntryAssembly().FullName.Substring(0, System.Reflection.Assembly.GetEntryAssembly().FullName.IndexOf(','));
            }
        }
    }
}
