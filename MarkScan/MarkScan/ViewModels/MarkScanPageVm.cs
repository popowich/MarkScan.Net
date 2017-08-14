using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MarkScan.Models;

namespace MarkScan.ViewModels
{
    internal class MarkScanPageVm
    {
        private Pages.MarkScanPage _markScanPage;
        private Models.MarkScanPageModel _markScanPageModel;

        private ETypeOperations _typeOperations;
        private System.Windows.Forms.Timer _timer;

        internal MarkScanPageVm(Pages.MarkScanPage markScanPage, bool newInventory)
        {
            _markScanPage = markScanPage;
            _markScanPage.barcodeTx.TextChanged += barcodeTx_TextChanged;
            _markScanPage.barcodeTx.KeyDown += textBox_KeyDown;

           _markScanPageModel = new MarkScanPageModel(newInventory);

            _timer = new System.Windows.Forms.Timer();
            _timer.Tick += _timer_Tick;
            _timer.Interval = 500;

            _updateLables();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (_markScanPage.barcodeTx.Text.Length > 0 
                && _markScanPageModel.ValidExciseStamp(_markScanPage.barcodeTx.Text) == false)
            {
                _timer.Stop();

                _blincTextBox();
            }
        }

        private void _blincTextBox()
        {
            int countBlink = 0;

            _markScanPage.barcodeTx.IsEnabled = false;

            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
            t.Interval = 250;
            t.Start();
            t.Tick += (object sender, EventArgs e) =>
            {
                if (countBlink % 2 == 0)
                {
                    _markScanPage.barcodeTx.Background = Brushes.Red;
                }
                else
                {
                    _markScanPage.barcodeTx.Background = Brushes.White;
                }

                if (countBlink == 3)
                {
                    _markScanPage.barcodeTx.Text = "";
                    _markScanPage.barcodeTx.IsEnabled = true;
                    t.Stop();
                    t.Dispose();
                }

                countBlink++;
            };
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            _timer.Stop();
            _timer.Start();
        }

        private void barcodeTx_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_markScanPageModel.ValidExciseStamp((_markScanPage.barcodeTx.Text)))
            {
                _markScanPage.barcodeTx.TextChanged -= barcodeTx_TextChanged;

                _markScanPage.barcodeTx.Text = Tools.KeyboardMapper.TranslateFromCyrillic(_markScanPage.barcodeTx.Text);
                _markScanPage.barcodeTx.SelectionStart = _markScanPage.barcodeTx.Text.Length;

                _markScanPageModel.HandleExciseStamp(_markScanPage.barcodeTx.Text);

                _markScanPage.barcodeTx.IsEnabled = false;

                _markScanPage.barcodeTx.Text = "";
                _markScanPage.barcodeTx.IsEnabled = true;

                _markScanPage.barcodeTx.TextChanged += barcodeTx_TextChanged;

                _updateLables();
            }

        }

        private void _updateLables()
        {
            int i = 0;
            for (int x = _markScanPageModel._scanResults.Count - 1; x >= 0; x--)
            {
                if (i == 0)
                {
                    _markScanPage.label2.Content = _markScanPageModel._scanResults[x].ExciseStamp;
                    _markScanPage.label1.Visibility = Visibility.Visible;
                    _markScanPage.label2.Visibility = Visibility.Visible;
                }
                if (i == 1)
                {
                    _markScanPage.label4.Content = _markScanPageModel._scanResults[x].ExciseStamp;
                    _markScanPage.label3.Visibility = Visibility.Visible;
                    _markScanPage.label4.Visibility = Visibility.Visible;
                }
                if (i == 2)
                {
                    _markScanPage.label6.Content = _markScanPageModel._scanResults[x].ExciseStamp;
                    _markScanPage.label5.Visibility = Visibility.Visible;
                    _markScanPage.label6.Visibility = Visibility.Visible;
                    break;
                }

                i++;
            }
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
