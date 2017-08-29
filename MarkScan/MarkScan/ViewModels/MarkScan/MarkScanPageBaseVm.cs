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
        protected IMarkScanModel _markScanPageModel;
        protected System.Windows.Forms.Timer _validateEnterTimer;
        protected System.Windows.Forms.Timer _blinkTimer;
        protected System.Windows.Forms.Timer _serviceMassegTimer;

        private bool _modeDeleteMark = false;
        private bool showWindowForScan = false;

        public MarkScanPageBaseVm(IMarkScanModel markScanPageModel)
        {
            _markScanPageModel = markScanPageModel;

            _validateEnterTimer = new System.Windows.Forms.Timer();
            _validateEnterTimer.Tick += _timer_Tick;
            _validateEnterTimer.Interval = 500;

            RetailEquipment.HidSacnerManager.hidScaner.ReadDataEvent += hidScaner_ReadDataEvent;
        }

        private void hidScaner_ReadDataEvent(object sender, RetailEquipment.HIDBarcodeReaderEventArgs e)
        {
            App._mainWindowsVm._MainWindow.Dispatcher.Invoke((Action)delegate
           {
               if (showWindowForScan)
                   App._mainWindowsVm.SetWindowState();

               _markScanPage.barcodeTx.Text = e.Barcode.ToUpper();
           });
        }


        public void SetMyPage(Pages.MarkScanPage markScanPage)
        {
            _markScanPage = markScanPage;
            _markScanPage.barcodeTx.TextChanged += barcodeTx_TextChanged;
            _markScanPage.barcodeTx.KeyDown += textBox_KeyDown;
            _markScanPage.serviceMessageLb.Content = "";
            _markScanPage.serviceMessageLb.Visibility = Visibility.Hidden;

            _updateLables();
            _updateCountScan();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (_markScanPage.barcodeTx.Text.Length > 0
                && _markScanPageModel.ValidExciseMark(_markScanPage.barcodeTx.Text) == false)
            {
                _validateEnterTimer.Stop();

                _blincTextBox();
            }
        }

        private void _blincTextBox()
        {
            int countBlink = 0;

            _markScanPage.barcodeTx.IsEnabled = false;

            if (_blinkTimer != null)
                _blinkTimer.Dispose();

            _blinkTimer = new System.Windows.Forms.Timer();
            _blinkTimer.Interval = 250;
            _blinkTimer.Start();

            _blinkTimer.Tick += (object sender, EventArgs e) =>
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
                    _markScanPage.barcodeTx.Focus();

                    _blinkTimer.Stop();
                    _blinkTimer.Dispose();
                }

                countBlink++;
            };
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            _validateEnterTimer.Stop();
            _validateEnterTimer.Start();
        }

        private void barcodeTx_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_markScanPageModel.ValidExciseMarkForLength((_markScanPage.barcodeTx.Text)))
            {
                _markScanPage.barcodeTx.TextChanged -= barcodeTx_TextChanged;

                _markScanPage.barcodeTx.Text = Tools.KeyboardMapper.TranslateFromCyrillic(_markScanPage.barcodeTx.Text);
                _markScanPage.barcodeTx.SelectionStart = _markScanPage.barcodeTx.Text.Length;

                if (_markScanPageModel.ValidExciseMark(_markScanPage.barcodeTx.Text) || _modeDeleteMark == true)
                {
                    if (_modeDeleteMark)
                    {
                        var result = _markScanPageModel.DeleteExciseMark(_markScanPage.barcodeTx.Text);
                        if (result)
                        {
                            _setServiceMasseg("Ранее отсканированная бутылка удалена из документа", Brushes.Green);
                        }
                        else
                        {
                            _setServiceMasseg("Ранее данная бутылка не сканировалась", Brushes.OrangeRed);
                        }

                        SetModeEnterMark(false);
                    }
                    else
                    {
                        var alkod = _markScanPageModel.HandleExciseMark(_markScanPage.barcodeTx.Text);

                        if (!string.IsNullOrEmpty(alkod))
                            _setTextNotification(true, alkod, false);
                        else
                        {
                            _setTextNotification(false, null, false);
                        }
                    }

                    _markScanPage.barcodeTx.IsEnabled = false;

                    _markScanPage.barcodeTx.Text = "";
                    _markScanPage.barcodeTx.IsEnabled = true;
                    _markScanPage.barcodeTx.Focus();

                    _updateLables();
                    _updateCountScan();
                }
                else
                {
                    _blincTextBox();
                    bool isDouble = _markScanPageModel.IsDuplicationMark(_markScanPage.barcodeTx.Text);
                    _setTextNotification(false, null, isDouble);
                }

                _markScanPage.barcodeTx.TextChanged += barcodeTx_TextChanged;

            }
        }

        private void _updateLables()
        {
            if (_markScanPageModel.ScanResults.Count < 3)
            {
                _markScanPage.label1.Visibility = Visibility.Hidden;
                _markScanPage.label2.Visibility = Visibility.Hidden;

                _markScanPage.mark1Lb.Visibility = Visibility.Hidden;
                _markScanPage.mark1DestLb.Visibility = Visibility.Hidden;

                _markScanPage.mark2Lb.Visibility = Visibility.Hidden;
                _markScanPage.mark2DestLb.Visibility = Visibility.Hidden;
            }

            int i = 0;
            for (int x = _markScanPageModel.ScanResults.Count - 1; x >= 0; x--)
            {
                if (i == 0)
                {
                    _markScanPage.mark1Lb.Content = _markScanPageModel.ScanResults[x].ExciseStamp;
                    _markScanPage.mark1Lb.Visibility = Visibility.Visible;

                    _markScanPage.mark1DestLb.Content =
                        _markScanPageModel.GetDescriptionAlcCode(_markScanPageModel.ScanResults[x].AlcCode);
                    _markScanPage.mark1DestLb.Visibility = Visibility.Visible;
                }
                if (i == 1)
                {
                    _markScanPage.mark2Lb.Content = _markScanPageModel.ScanResults[x].ExciseStamp;
                    _markScanPage.mark2Lb.Visibility = Visibility.Visible;

                    _markScanPage.mark2DestLb.Content =
                        _markScanPageModel.GetDescriptionAlcCode(_markScanPageModel.ScanResults[x].AlcCode);
                    _markScanPage.mark2DestLb.Visibility = Visibility.Visible;

                    break;
                }

                i++;
            }
        }

        private void _updateCountScan()
        {
            _markScanPage.countScan.Content = _markScanPageModel.ScanResults.Count;
        }

        private void _setServiceMasseg(string text, Brush color)
        {
            _markScanPage.serviceMessageLb.Content = text;
            _markScanPage.serviceMessageLb.Foreground = color;
            _markScanPage.serviceMessageLb.Visibility = Visibility.Visible;

            _delayedCleanServiceMassege();
        }

        private void _delayedCleanServiceMassege()
        {
            if (_serviceMassegTimer != null)
                _serviceMassegTimer.Dispose();

            _serviceMassegTimer = new System.Windows.Forms.Timer();
            _serviceMassegTimer.Interval = 3000;
            _serviceMassegTimer.Start();

            _serviceMassegTimer.Tick += (object sender, EventArgs e) =>
            {
                _serviceMassegTimer.Stop();

                SetModeEnterMark(true);

                _serviceMassegTimer.Dispose();
            };
        }

        private void _setTextNotification(bool isGood, string alcod, bool isDuble)
        {
            if (App._mainWindowsVm._MainWindow.IsVisible == false)
                if (isGood)
                    App._mainWindowsVm._MainWindow.notify_icon.ShowBalloonTip(1, alcod, "Принято", System.Windows.Forms.ToolTipIcon.Info);
                else
                    if (isDuble)
                       App._mainWindowsVm._MainWindow.notify_icon.ShowBalloonTip(1, "Дублирование марки", "Марка", System.Windows.Forms.ToolTipIcon.Warning);
                    else
                    App._mainWindowsVm._MainWindow.notify_icon.ShowBalloonTip(1, "Ввод отклонен", "Марка", System.Windows.Forms.ToolTipIcon.Warning);

        }

        #region Operations

        /// <summary>
        /// Установить режим удаления марки
        /// </summary>
        public void SetModeDeleteMark()
        {
            if (_modeDeleteMark)
            {
                SetModeEnterMark(true);
                return;
            }

            _markScanPage.serviceMessageLb.Content = "Готов произвести возврат";
            _markScanPage.deleteMark.Content = "Отменить удаление";
            _markScanPage.deleteMark.Background = Brushes.OrangeRed;

            _markScanPage.serviceMessageLb.Visibility = Visibility.Visible;

            _markScanPage.barcodeTx.Focus();

            _modeDeleteMark = true;
        }
        /// <summary>
        /// Установить режим ввода марки
        /// </summary>
        /// <param name="clearMess"></param>
        public void SetModeEnterMark(bool clearMess)
        {
            if (clearMess)
            {
                _markScanPage.serviceMessageLb.Content = "";
                _markScanPage.serviceMessageLb.Foreground = Brushes.Black;
                _markScanPage.serviceMessageLb.Visibility = Visibility.Hidden;
            }

            _markScanPage.deleteMark.Content = "Удалить позицию";
            _markScanPage.deleteMark.Background = new SolidColorBrush(Color.FromRgb(126, 126, 126));

            _markScanPage.barcodeTx.Focus();

            _modeDeleteMark = false;
        }
        /// <summary>
        /// Установить режим отображения формы при сканировании
        /// </summary>
        public void SetModeShowWindowForScan()
        {
            showWindowForScan = !showWindowForScan;
            if (showWindowForScan)
            {
                _markScanPage.showWindowForScanGrid.Background = new SolidColorBrush(Color.FromRgb(15, 111, 198));
            }
            else
            {
                _markScanPage.showWindowForScanGrid.Background = new SolidColorBrush(Color.FromRgb(126, 126, 126));
            }

            _markScanPage.barcodeTx.Focus();
        }

        #endregion
    }
}
