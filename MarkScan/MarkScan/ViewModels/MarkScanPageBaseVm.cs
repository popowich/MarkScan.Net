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
    public abstract class MarkScanPageBaseVm
    {
        protected Pages.MarkScanPage _markScanPage;
        protected IMarkScanPageModel _markScanPageModel;
        protected System.Windows.Forms.Timer _timer;

        public MarkScanPageBaseVm( IMarkScanPageModel markScanPageModel)
        {
            _markScanPageModel = markScanPageModel;

            _timer = new System.Windows.Forms.Timer();
            _timer.Tick += _timer_Tick;
            _timer.Interval = 500;
        }

        public void SetOwnerPage(Pages.MarkScanPage markScanPage)
        {
            _markScanPage = markScanPage;
            _markScanPage.barcodeTx.TextChanged += barcodeTx_TextChanged;
            _markScanPage.barcodeTx.KeyDown += textBox_KeyDown;

            _updateLables();
            _updateCountScan();
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
                _updateCountScan();
            }

        }

        private void _updateLables()
        {
            int i = 0;
            for (int x = _markScanPageModel.ScanResults.Count - 1; x >= 0; x--)
            {
                if (i == 0)
                {
                    _markScanPage.label2.Content = _markScanPageModel.ScanResults[x].ExciseStamp;
                    _markScanPage.label1.Visibility = Visibility.Visible;
                    _markScanPage.label2.Visibility = Visibility.Visible;
                }
                if (i == 1)
                {
                    _markScanPage.label4.Content = _markScanPageModel.ScanResults[x].ExciseStamp;
                    _markScanPage.label3.Visibility = Visibility.Visible;
                    _markScanPage.label4.Visibility = Visibility.Visible;
                }
                if (i == 2)
                {
                    _markScanPage.label6.Content = _markScanPageModel.ScanResults[x].ExciseStamp;
                    _markScanPage.label5.Visibility = Visibility.Visible;
                    _markScanPage.label6.Visibility = Visibility.Visible;
                    break;
                }

                i++;
            }
        }

        private void _updateCountScan()
        {
            _markScanPage.countScan.Content = _markScanPageModel.ScanResults.Count;
        }
    }
}
