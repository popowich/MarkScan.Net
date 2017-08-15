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
    public partial class OperationMenuPage : System.Windows.Controls.Page
    {
        private IOperationMenuVm _operationMenuVm;

        public OperationMenuPage(IOperationMenuVm operationMenuVm)
        {
            InitializeComponent();

            _operationMenuVm = operationMenuVm;
        }

        private void backPage_Click(object sender, RoutedEventArgs e)
        {
            _operationMenuVm.GoToMainMenuPage();
        }

        private void newBt_Click(object sender, RoutedEventArgs e)
        {
            _operationMenuVm.GoToMarkScanNew();
        }

        private void continuebt_Click(object sender, RoutedEventArgs e)
        {
            _operationMenuVm.GoToMarkScanСontinue();
        }

        private void sendDataBt_Click(object sender, RoutedEventArgs e)
        {
            _operationMenuVm.SendDatatoCvC();
        }
    }
}
