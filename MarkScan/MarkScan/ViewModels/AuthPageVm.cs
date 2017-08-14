using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            return string.IsNullOrEmpty(AppSettings.settings.Login)
                   || string.IsNullOrEmpty(AppSettings.settings.Pass);
        }

        internal bool Auth(string login, string pass)
        {
            try
            {
                var result = Network.CvcOpenApi.GetClientApi().Auth(login, pass);

                if (result.Response.Code == 200)
                {
                    AppSettings.settings.Login = login;
                    AppSettings.settings.Pass = pass;
                    AppSettings.settings.SaveSetting();

                    return true;
                }
                else
                {
                    throw new Exception("Авторизация не выполнена: " + result.Response.LocalizedMessage);
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
