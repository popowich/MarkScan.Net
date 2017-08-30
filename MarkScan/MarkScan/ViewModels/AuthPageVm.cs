using System;

namespace MarkScan.ViewModels
{
    internal class AuthPageVm
    {
        private Pages.AuthPage _authPage;

        internal AuthPageVm(Pages.AuthPage authPage)
        {
            _authPage = authPage;
        }

        internal bool IsPerfAuthoriation()
        {
            return string.IsNullOrEmpty(AppSettings._settings.Login)
                   || string.IsNullOrEmpty(AppSettings._settings.Pass);
        }

        internal bool Auth(string login, string pass)
        {
            try
            {
                var result = Network.CvcOpenApi.GetClientApi().Auth(login, pass);

                if (result.Response.Code == 200)
                {
                    AppSettings._settings.Login = login;
                    AppSettings._settings.Pass = pass;
                    AppSettings._settings.SaveSetting();

                    return true;
                }
                else
                {
                    throw new Exception("Авторизация не выполнена: LocalizedMessage: " + result.Response.LocalizedMessage + ", code: " + result.Response.Code );
                }
            }
            catch (Exception e)
            {           
                AppSettings.HandlerException(e);
                SetMessageError(e.Message);
            }

            return false;
        }

        internal void GoToMainMenuPage()
        {
            App._mainWindowsVm.GoToMainMenuPage();
        }

        internal void SetMessageError(string message)
        {
            _authPage.errorLb.Text = message;
        }
    }
}
