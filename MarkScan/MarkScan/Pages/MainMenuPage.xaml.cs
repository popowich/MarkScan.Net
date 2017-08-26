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
        private MainMenuPageVm _mainMenuVm;

        public MainMenuPage()
        {
            InitializeComponent();

            _mainMenuVm = new MainMenuPageVm(this);
        }

        private void inventoryBt_Click(object sender, RoutedEventArgs e)
        {
            _mainMenuVm.GoToInventoryOperationMenuPage();
        }

        private void writeOffBt_Click(object sender, RoutedEventArgs e)
        {
            _mainMenuVm.GoToWriteOffOperationMenuPage();
        }

        private void testConnectBt_Click(object sender, RoutedEventArgs e)
        {
            _mainMenuVm.TestConnect();
        }

        private void changeUserBt_Click(object sender, RoutedEventArgs e)
        {
            _mainMenuVm.GoToChangeAuth();
        }

        private void appWxitBt_Click(object sender, RoutedEventArgs e)
        {
            _mainMenuVm.Exit();
        }

        private void writeOffBerrBt_Click(object sender, RoutedEventArgs e)
        {
            App._mainWindowsVm._generalFrame.Navigate(new Pages.WriteOffBeerPage());
        }
    }
}
