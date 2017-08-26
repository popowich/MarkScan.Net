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

        static HidSacnerManager()
        {
            hidScaner = new HIDBarcodeReader(Keys.F7, Keys.Enter, 68);
        }

    }
}
