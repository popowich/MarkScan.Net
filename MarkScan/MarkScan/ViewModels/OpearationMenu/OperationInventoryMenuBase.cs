using System;
using System.Windows.Media;
using MarkScan.Pages;

namespace MarkScan.ViewModels
{
    public class OperationInventoryMenuBase
    {
        protected OperationMenuPage _operationMenuPage;
        protected System.Windows.Forms.Timer _timer;

        public void SetPage(OperationMenuPage operationMenuPage)
        {
            _operationMenuPage = operationMenuPage;
        }

        public void TestConnect()
        {
            try
            {
                var conn = Network.CvcOpenApi.GetClientApi();
                conn.TestConnect();

                _operationMenuPage.messageTxb.Text = "Соединение с сервером выполнено успешно!";
                _operationMenuPage.messageTxb.Foreground = Brushes.Green;
                _clearTextMessage();
            }
            catch (Exception e)
            {
                _operationMenuPage.messageTxb.Text = "Не удалось соединиться с сервером: " + e.Message;
                _operationMenuPage.messageTxb.Foreground = Brushes.OrangeRed;

                _clearTextMessage();
            }
        }

        private void _clearTextMessage()
        {
            if (_timer != null && _timer.Enabled == true)
            {
                _timer.Enabled = false;
                _timer.Dispose();
            }

            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 5000;
            _timer.Tick += (object sender, EventArgs e) =>
            {
                _operationMenuPage.messageTxb.Text = "";
                _timer.Dispose();
            };
            _timer.Start();
        }
    }
}
