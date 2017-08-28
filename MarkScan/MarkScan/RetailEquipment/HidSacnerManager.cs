using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MarkScan.RetailEquipment
{
    internal static class HidSacnerManager
    {
        internal static RetailEquipment.HIDBarcodeReader hidScaner;

        internal static bool IsReady { get; private set; }

        static HidSacnerManager()
        {
            IsReady = AppSettings.settings.Prefix != Keys.None && AppSettings.settings.Suffix != Keys.None;
            hidScaner = new HIDBarcodeReader(AppSettings.settings.Prefix, AppSettings.settings.Suffix, 68);
        }

    }
}
