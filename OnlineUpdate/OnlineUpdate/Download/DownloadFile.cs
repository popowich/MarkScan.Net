using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace OnlineUpdate.Download
{
    public class DownloadFileSync : IDownloadFile
    {
        public bool DownloadFile(string urlSource, string _patchFile)
        {
            new WebClient().DownloadFile(Uri.EscapeUriString(urlSource), _patchFile);

            return true;
        }

        public bool TryDownloadFile(string urlSource, string _patchFile)
        {
            int countTry = 0;

            while (countTry < 3)
            {
                try
                {
                    new WebClient().DownloadFile(urlSource, _patchFile);

                    return true;
                }
                catch (Exception ex)
                {
                    if (countTry == 2)
                        throw ex;
                    else
                        countTry++;
                }
            }

            return false;
        }
    }
}
