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
    /// Логика взаимодействия для QuaerePage.xaml
    /// </summary>
    public partial class QuaerePage : Page
    {
        private ViewModels.IQuaereVm _viewModelQuaereVm;

        public QuaerePage(ViewModels.IQuaereVm viewModelQuaereVm, string textQuaere)
        {
            InitializeComponent();

            _viewModelQuaereVm = viewModelQuaereVm;
            quaereText.Text = textQuaere;
        }

        private void yesBt_Click(object sender, RoutedEventArgs e)
        {
            _viewModelQuaereVm.HandleYesResult();
        }

        private void noBt_Click(object sender, RoutedEventArgs e)
        {
            _viewModelQuaereVm.HandleNoResult();
        }
    }
}
