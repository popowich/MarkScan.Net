using System;

namespace MarkScan.RetailEquipment
{
    public class HIDBarcodeReaderEventArgs : EventArgs
    {
        public string Barcode { get; set; }

        public HIDBarcodeReaderEventArgs(string data)
        {
            Barcode = data;
        }
    }
}
