using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Логика взаимодействия для MarkScanPage.xaml
    /// </summary>
    public partial class MarkScanPage : System.Windows.Controls.Page
    {
        private MarkScanPageVm _markScanPageVm;

        public MarkScanPage()
        {
            InitializeComponent();

            _markScanPageVm = new MarkScanPageVm(this);

            WrapPanel1.Visibility = Visibility.Hidden;
            WrapPanel2.Visibility = Visibility.Hidden;
            WrapPanel3.Visibility = Visibility.Hidden;

            barcodeTx.Focus();
        }

        internal void SetOpearation(ETypeOperations typeOperations)
        {
            _markScanPageVm.SetTypeeOperation(typeOperations);
        }

        private void backBt_Click(object sender, RoutedEventArgs e)
        {
            _markScanPageVm.GoToInventoryMenuPage();
        }

        private int count;

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            count += 1;
        }

        private void handle(string code)
        {
            string code2 = code.Substring(3, 16);

            var cc = conv(code2);
        }

        List<string> dd = new List<string>();

        private void barcodeTx_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (count == 68)
            {
                barcodeTx.IsEnabled = false;

                count = 0;
                dd.Add(barcodeTx.Text);

                int i = 0;
                for(int x = dd.Count - 1; x>=0;x--)
                {
                    if (i == 0)
                    {
                        label2.Content = dd[x];
                        WrapPanel1.Visibility = Visibility.Visible;
                    }
                    if (i == 1)
                    {
                        label4.Content = dd[x];
                        WrapPanel2.Visibility = Visibility.Visible;
                    }
                    if (i == 2)
                    {
                        label6.Content = dd[x];
                        WrapPanel3.Visibility = Visibility.Visible;
                        break;
                    }

                    i++;
                }
                barcodeTx.Clear();

                barcodeTx.IsEnabled = true;
                // handle(barcodeTx.Text);
            }

        }

        private string conv(string code )
        {
            double n = 0;
            double sum = 0;
            int count = code.Length - 1;

            foreach (var c in code)
            {
                if (c >= '0' && c <= '9')
                    n = (double)(c - '0') * (double)Math.Pow(36, count);
                else if (c >= 'A' && c <= 'Z')
                    n = (double)(c - 'A' + 10) * (double)Math.Pow(36, count);
                else if (c >= 'a' && c <= 'z')
                    n = (double)(c - 'a' + 10) * (double)Math.Pow(36, count);
                else
                    break;

                sum += n;
                n = 0;

                count--;
            }

            decimal d = (decimal) sum;

            return n.ToString();

        }
    }
}
