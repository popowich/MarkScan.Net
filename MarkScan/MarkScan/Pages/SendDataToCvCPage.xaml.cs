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

namespace MarkScan.Pages
{
    /// <summary>
    /// Логика взаимодействия для SendDataToCvCPage.xaml
    /// </summary>
    public partial class SendDataToCvCPage : Page
    {
        private ViewModels.ISendDataToCvCVm _sendDataToCvCVm;

        public SendDataToCvCPage(ViewModels.ISendDataToCvCVm sendDataToCvCVm)
        {
            InitializeComponent();

            _sendDataToCvCVm = sendDataToCvCVm;
            _sendDataToCvCVm.SetPage(this);

            this.Loaded += SendDataToCvCPage_Loaded;
        }

        private void SendDataToCvCPage_Loaded(object sender, RoutedEventArgs e)
        {
            _sendDataToCvCVm.SendData();
        }

        private void backPage_Click(object sender, RoutedEventArgs e)
        {
            _sendDataToCvCVm.GoToOperationMenu();
        }
    }
}
