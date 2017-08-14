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
    /// Логика взаимодействия для MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : System.Windows.Controls.Page
    {
        private MainMenuPageVm _mainMenuVm = new MainMenuPageVm();

        public MainMenuPage()
        {
            InitializeComponent();
        }

        private void inventoryBt_Click(object sender, RoutedEventArgs e)
        {
            _mainMenuVm.GoToInventoryMenuPage();
        }

        private void appWxitBt_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void changeUserBt_Click(object sender, RoutedEventArgs e)
        {
            //App._mainWindowsVm._generalFrame.Source =
            //    new Uri(@"pack://application:,,,/" + AppSettings.NameAssembly + ";component/Pages/AuthPage.xaml",
            //        UriKind.Absolute);
            // App._mainWindowsVm._generalFrame.Source = null;

            App._mainWindowsVm._generalFrame.Navigate(new Pages.AuthPage(true));
        }
    }
}
