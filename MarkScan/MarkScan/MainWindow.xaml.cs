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
using MarkScan.Network;
using MarkScan.ViewModels;

namespace MarkScan
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Объект иконки в трее
        /// </summary>
        public System.Windows.Forms.NotifyIcon notify_icon;

        public MainWindow()
        {
            InitializeComponent();

            App._mainWindowsVm._generalFrame = generalFrame;
            App._mainWindowsVm._MainWindow = this;
            App._mainWindowsVm.GoToAuthPage();

            App._mainWindowsVm.SetVersion();


            notify_icon = new System.Windows.Forms.NotifyIcon();
            notify_icon.Icon = Properties.Resources.CVC;
            notify_icon.Text = "Mark Scan.Net";

            notify_icon.MouseDoubleClick += notify_icon_MouseDoubleClick;
            System.Windows.Forms.ContextMenuStrip menu = new System.Windows.Forms.ContextMenuStrip();
            menu.Items.Add("Открыть форму", null, notify_icon_show_form);

            menu.Items.Add(new System.Windows.Forms.ToolStripSeparator());
            menu.Items.Add("Выход", null, notify_icon_exit);
            notify_icon.ContextMenuStrip = menu;
            notify_icon.Visible = true;
        }


        private void image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            App._mainWindowsVm.ChekUpdate();
        }


        #region Handler eventes icon tray

        /// <summary>
        /// Обработчик двойного клика иконки трея
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notify_icon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            App._mainWindowsVm.SetWindowState();
        }
        /// <summary>
        /// Обработчик  меню трея открыть окно главной формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notify_icon_show_form(object sender, EventArgs e)
        {
            App._mainWindowsVm.SetWindowState();
        }
        /// <summary>
        /// Обработчик  меню трея закрыть программу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notify_icon_exit(object sender, EventArgs e)
        {
            notify_icon.Visible = false;
            App.Current.Shutdown();
        }

        #endregion

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
                this.Hide();
        }


    }
}
