using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CreatorReleases
{
    public static class Utilites
    {
        public static string GetFilesMD5(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] filebytes = new byte[fs.Length];
                fs.Read(filebytes, 0, (int)fs.Length);
                fs.Close();
                byte[] Sum = md5.ComputeHash(filebytes);
                string result = BitConverter.ToString(Sum).Replace("-", String.Empty);
                return result;
            }
        }
    }
}
