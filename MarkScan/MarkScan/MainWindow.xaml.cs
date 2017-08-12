﻿using System;
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
using MarkScan.Network;
using MarkScan.ViewModels;

namespace MarkScan
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindowsVm viewModel;

        public MainWindow()
        {
            InitializeComponent();

            viewModel = new MainWindowsVm();
            viewModel.generalFrame = generalFrame;

            generalFrame.Navigate(new Uri(@"pack://application:,,,/" + AppSettings.NameAssembly + ";component/Pages/AuthPage.xaml", UriKind.Absolute));
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            new CvcOpenApi().Auth("login", "pass");
        }
    }
}
