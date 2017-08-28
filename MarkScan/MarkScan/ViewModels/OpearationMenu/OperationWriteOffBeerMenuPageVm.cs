﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkScan.ViewModels.OpearationMenu
{
    public class OperationWriteOffBeerMenuPageVm : OperationInventoryMenuBase, IOperationMenuVm
    {
        public OperationWriteOffBeerMenuPageVm()
        {
            App._mainWindowsVm._MainWindow.Title = "Mark Scan.Net - списание пива";
        }

        public void GoToMainMenuPage()
        {
            App._mainWindowsVm._MainWindow.Title = "Mark Scan.Net";
            App._mainWindowsVm._generalFrame.Navigate(new Uri(@"pack://application:,,,/" + AppSettings.NameAssembly + ";component/Pages/MainMenuPage.xaml", UriKind.Absolute));
        }

        public void GoToMarkScanNew()
        {

        }

        public void GoToMarkScanСontinue()
        {
         
        }

        public void SendDatatoCvC()
        {
         
        }
    }
}
