using System.Windows.Forms;

namespace MarkScan.RetailEquipment
{
    internal static class HidSacnerManager
    {
        internal static RetailEquipment.HIDBarcodeReader _hidScaner;

        internal static bool IsReady { get; private set; }

        static HidSacnerManager()
        {
            IsReady = AppSettings._settings.Prefix != Keys.None && AppSettings._settings.Suffix != Keys.None;

            _hidScaner = new HIDBarcodeReader(AppSettings._settings.Prefix, AppSettings._settings.Suffix, 68);
        }

    }
}
