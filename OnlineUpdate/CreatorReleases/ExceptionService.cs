using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatorReleases
{
    public static class ExceptionService
    {
        internal static Exception LastError { get; set; }

        static ExceptionService()
        {

        }

        internal static void Except(Exception ex, bool showError = true)
        {
            LastError = ex;

            if (showError)
            {
               new ExceptionForm(ex).ShowDialog();
            }
        }

    }

}
