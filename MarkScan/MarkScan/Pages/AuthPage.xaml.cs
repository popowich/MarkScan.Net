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

namespace MarkScan.Page
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : System.Windows.Controls.Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void authBt_Click(object sender, RoutedEventArgs e)
        {
            Network.CvcOpenApi.GetClientApi().Auth(loginTx.Text, passTx.Text);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //ResultScanPosititon pp = new ResultScanPosititon();
            //pp.Positions = new ResultScan[]
            //{
            //    new ResultScan() {AlcCode = "0177379000001560990", Quantity = 8}
            //};

            //Network.CvcOpenApi.GetClientApi().Writeoff(pp);

            MainWindow.viewModel.generalFrame.Navigate(new Uri(@"pack://application:,,,/" + AppSettings.NameAssembly + ";component/Pages/MainMenuPage.xaml", UriKind.Absolute));
        }
    }
}
