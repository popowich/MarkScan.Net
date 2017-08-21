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

namespace MarkScan.Pages
{
    /// <summary>
    /// Логика взаимодействия для AppUpdateDescriptionPage.xaml
    /// </summary>
    public partial class AppUpdateDescriptionPage : Page
    {
        private ViewModels.AppUpdateDescriptopnVm _myPresenter;

        public AppUpdateDescriptionPage(ViewModels.AppUpdateDescriptopnVm myPresenter)
        {
            InitializeComponent();

            _myPresenter = myPresenter;

            Loaded += AppUpdateDescriptionPage_Loaded;
        }

        private void AppUpdateDescriptionPage_Loaded(object sender, RoutedEventArgs e)
        {
            _myPresenter.SetPage(this);
        }

        private void yesBt_Click(object sender, RoutedEventArgs e)
        {
            _myPresenter.GoToUpdate();
        }

        private void noBt_Click(object sender, RoutedEventArgs e)
        {
            _myPresenter.CancelUpdate();
        }
    }
}
