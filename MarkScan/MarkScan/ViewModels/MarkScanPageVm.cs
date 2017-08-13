using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkScan.ViewModels
{
    internal class MarkScanPageVm
    {
        private Pages.MarkScanPage _markScanPage;
        private ETypeOperations _typeOperations;

        internal MarkScanPageVm(Pages.MarkScanPage markScanPage)
        {
            _markScanPage = markScanPage;
        }

        internal void GoToInventoryMenuPage()
        {
            App._mainWindowsVm._generalFrame.Navigate(new Uri(@"pack://application:,,,/" + AppSettings.NameAssembly + ";component/Pages/InventoryMenuPage.xaml", UriKind.Absolute));
        }

        internal void SetTypeeOperation(ETypeOperations typeOperations)
        {
            _typeOperations = typeOperations;

            if (typeOperations == ETypeOperations.Inventory)
                _markScanPage.nameOperation.Content = "Инвентаризация";
            else
                _markScanPage.nameOperation.Content = "Списание";
        }
    }
}
