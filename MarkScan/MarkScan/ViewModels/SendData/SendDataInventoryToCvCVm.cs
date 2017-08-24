using System;
using System.Windows.Media;

namespace MarkScan.ViewModels
{
    public class SendDataInventoryToCvCVm : ISendDataToCvCVm
    {
        private Pages.SendDataToCvCPage _page;
        protected System.Windows.Forms.Timer _timer;

        public void SendData()
        {
            try
            {
                var model = new Models.MarkScanInventoryModel(false);

                if (model.ScanResults.Count == 0)
                {
                    _page.messageTxb.Text = "Нет данных для отправки!";
                    _goBack();
                    return;
                }

                if (model.SendToCvC())
                {
                    _page.messageTxb.Text = "Инвентаризация отправлена успешно!";
                    _page.messageTxb.Foreground = Brushes.Green;
                    model.ClearScanData();
                }
                else
                {
                    _page.messageTxb.Text = "Ошибка отправки инвентаризации";
                    _page.messageTxb.Foreground = Brushes.OrangeRed;
                }
  
            }
            catch (Exception e)
            {
                _page.messageTxb.Text = "Ошибка отправки инвентаризации: " + e.Message;
                _page.messageTxb.Foreground = Brushes.OrangeRed;

                AppSettings.SaveDebug("Ошибка отправки инветаризации");
                AppSettings.HandlerException(e);
            }

            _goBack();
        }

        public void SetPage(Pages.SendDataToCvCPage page)
        {
            _page = page;
        }

        public void GoToOperationMenu()
        {
            if (_timer != null && _timer.Enabled == true)
            {
                _timer.Enabled = false;
                _timer.Dispose();
            }

            App._mainWindowsVm._generalFrame.Navigate(new Pages.OperationMenuPage(new OperationInventoryMenuPageVm()));
        }

        private void _goBack()
        {
            if (_timer != null && _timer.Enabled == true)
            {
                _timer.Enabled = false;
                _timer.Dispose();
            }

            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 7000;
            _timer.Tick += (object sender, EventArgs e) =>
            {
                App._mainWindowsVm._generalFrame.Navigate(new Pages.OperationMenuPage(new OperationInventoryMenuPageVm()));
                _timer.Dispose();
            };
            _timer.Start();
        }
    }
}
