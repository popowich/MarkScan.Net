using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MarkScan.ViewModels;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace MarkScan.Pages
{
    /// <summary>
    /// Логика взаимодействия для MarkScanPage.xaml
    /// </summary>
    public partial class MarkScanPage : System.Windows.Controls.Page
    {
        private IMarkScanPageVm _markScanPageVm;

        public MarkScanPage(IMarkScanPageVm markScanPageVm)
        {
            InitializeComponent();

            label1.Visibility = Visibility.Hidden;
            label2.Visibility = Visibility.Hidden;
            label3.Visibility = Visibility.Hidden;
            label4.Visibility = Visibility.Hidden;
            label5.Visibility = Visibility.Hidden;
            label6.Visibility = Visibility.Hidden;

            barcodeTx.Focus();

            _markScanPageVm = markScanPageVm;
            _markScanPageVm.SetOwnerPage(this);

        }

        private void backBt_Click(object sender, RoutedEventArgs e)
        {
            _markScanPageVm.GoToOpearationMenuPage();
        }

        private void deleteMarkbutton_Click(object sender, RoutedEventArgs e)
        {
            _markScanPageVm.SetModeDeleteMark();
        }
    }
}
