using ICSharpCode.SharpZipLib.BZip2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OnlineUpdate
{
    internal static class FileUtilities
    {
        internal static void DecompressBz2(string sourceFile, string descFile)
        {
            //Распаковка файла
            FileInfo fileRead = new FileInfo(sourceFile);
            FileInfo fileWrite = new FileInfo(descFile);
            using (FileStream rs = fileRead.OpenRead())
            {
                using (FileStream ws = fileWrite.Create())
                {
                    BZip2.Decompress(rs, ws);
                }
            }
        }
    }
}
