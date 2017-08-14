using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MarkScan.ViewModels;

namespace MarkScan.Pages
{
    /// <summary>
    /// Логика взаимодействия для InventoryPage.xaml
    /// </summary>
    public partial class InventoryMenuPage : System.Windows.Controls.Page
    {
        private InventoryMenuPageVm _inventoryMenuPageVm = new InventoryMenuPageVm();

        public InventoryMenuPage()
        {
            InitializeComponent();
        }

        private void backPage_Click(object sender, RoutedEventArgs e)
        {
            _inventoryMenuPageVm.GoToMainMenuPage();
        }

        private void newBt_Click(object sender, RoutedEventArgs e)
        {
            _inventoryMenuPageVm.GoToMarkScanNew();
        }

        private void continuebt_Click(object sender, RoutedEventArgs e)
        {
            _inventoryMenuPageVm.GoToMarkScanСontinue();
        }

        private void sendDataBt_Click(object sender, RoutedEventArgs e)
        {
            new Models.MarkScanPageModel(false)._sendToCvC();
        }
    }
}
