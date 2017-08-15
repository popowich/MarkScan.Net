
using System;
using System.Windows.Media;
using MarkScan.Pages;

namespace MarkScan.ViewModels
{
    class MainMenuPageVm
    {
        private MainMenuPage _mainMenuPage;
        private System.Windows.Forms.Timer _timer;

        internal MainMenuPageVm(MainMenuPage mainMenuPage)
        {
            _mainMenuPage = mainMenuPage;
        }

        internal void GoToInventoryOperationMenuPage()
        {
            App._mainWindowsVm._generalFrame.Navigate(new Pages.OperationMenuPage(new ViewModels.OperationInventoryMenuPageVm()));
        }

        internal void GoToWriteOffOperationMenuPage()
        {
            App._mainWindowsVm._generalFrame.Navigate(new Pages.OperationMenuPage(new ViewModels.OperationWriteOffMenuPageVm()));
        }

        internal void TestConnect()
        {
            try
            {
                var conn = Network.CvcOpenApi.GetClientApi();
                conn.TestConnect();

                _mainMenuPage.messageTxb.Text = "Соединение с сервером выполнено успешно!";
                _mainMenuPage.messageTxb.Foreground = Brushes.Green;
                _clearTextMessage();
            }
            catch (Exception e)
            {
                _mainMenuPage.messageTxb.Text = "Не удалось соединиться с сервером: " + e.Message;
                _mainMenuPage.messageTxb.Foreground = Brushes.OrangeRed;

                _clearTextMessage();
            }
        }

        internal void GoToChangeAuth()
        {
            App._mainWindowsVm._generalFrame.Navigate(new Pages.QuaerePage(new ViewModels.QuaereChandeUserVm(), "Вы действительно хотите сменить пользователя ?"));
        }

        internal void Exit()
        {
            App.Current.Shutdown();
        }

        private void _clearTextMessage()
        {
            if (_timer != null&& _timer.Enabled == true)
            {
                _timer.Enabled = false;
                _timer.Dispose();
            }

            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 5000;
            _timer.Tick += (object sender, EventArgs e) =>
            {
                _mainMenuPage.messageTxb.Text = "";
                _timer.Dispose();
            };
            _timer.Start();
        }


    }
}
