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
        private MarkScanPageVm _markScanPageVm;
        private System.Windows.Forms.Timer _timer;

        public MarkScanPage()
        {
            InitializeComponent();

            _markScanPageVm = new MarkScanPageVm(this);

            WrapPanel1.Visibility = Visibility.Hidden;
            WrapPanel2.Visibility = Visibility.Hidden;
            WrapPanel3.Visibility = Visibility.Hidden;

            barcodeTx.Focus();

            _timer = new System.Windows.Forms.Timer();
            _timer.Tick += _timer_Tick;
            _timer.Interval = 500;
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
            _timer.Stop();
            _timer.Start();
        }

        private void handle(string code)
        {
            string code2 = code.Substring(3, 16);

            var cc = conv(code2);
        }

        List<string> dd = new List<string>();

        private void barcodeTx_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (barcodeTx.Text.Length == 68)
            {
                barcodeTx.TextChanged -= barcodeTx_TextChanged;

                barcodeTx.Text = Tools.KeyboardMapper.TranslateFromCyrillic(barcodeTx.Text);
                barcodeTx.SelectionStart = barcodeTx.Text.Length;

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

                handle(barcodeTx.Text);

                barcodeTx.Text = "";
                barcodeTx.IsEnabled = true;

                barcodeTx.TextChanged += barcodeTx_TextChanged;

            }

        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            if(barcodeTx.Text.Length > 0 && barcodeTx.Text.Length != 68)
            {
                _timer.Stop();

                migati();
            }
        }

        private void migati()
        {
            int count = 0;

            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
            t.Interval = 250;
            t.Start();
            t.Tick += (object sender, EventArgs e) =>
            {
                if (count % 2 == 0)
                {
                    barcodeTx.Background = Brushes.Red;
                }
                else
                {
                    barcodeTx.Background = Brushes.White;
                }

                if (count == 3)
                {
                    barcodeTx.Text = "";
                    t.Stop();
                    t.Dispose();
                }

                count++;
            };
        }

        private string conv(string code )
        {
            BigInteger bigIntFromDouble = new BigInteger(0);

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

                bigIntFromDouble = bigIntFromDouble + new BigInteger(n);
                sum += n;
                n = 0;

                count--;
            }

            MessageBox.Show(bigIntFromDouble.ToString());

            return bigIntFromDouble.ToString();

        }

        private void barcodeTx_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void barcodeTx_PreviewKeyUp(object sender, KeyEventArgs e)
        {
           
        }
    }
}
