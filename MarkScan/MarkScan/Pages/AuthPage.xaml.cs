using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : System.Windows.Controls.Page
    {
        private AuthPageVm _viewModelAuthPageVm;

        public AuthPage()
        {
            InitializeComponent();

            _viewModelAuthPageVm = new AuthPageVm(this);

            if (!_viewModelAuthPageVm.IsPerfAuthoriation())
                _viewModelAuthPageVm.GoToMainMenuPage();
        }

        public AuthPage(bool handle)
        {
            InitializeComponent();

            _viewModelAuthPageVm = new AuthPageVm(this);
        }

        private void authBt_Click(object sender, RoutedEventArgs e)
        {
           if(_viewModelAuthPageVm.Auth(loginTx.Text, passTx.Text) == true)
               _viewModelAuthPageVm.GoToMainMenuPage();
        }
    }
}
