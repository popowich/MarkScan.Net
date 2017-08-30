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
        public System.Windows.Forms.NotifyIcon _notify_icon;

        private ViewModels.MainWindowsVm _mainWindowsVm = App._mainWindowsVm;

        public MainWindow()
        {
            InitializeComponent();

            _mainWindowsVm._generalFrame = generalFrame;
            _mainWindowsVm._mainWindow = this;
            _mainWindowsVm.GoToAuthPage();

            _mainWindowsVm.SetVersion();


            _notify_icon = new System.Windows.Forms.NotifyIcon();
            _notify_icon.Icon = Properties.Resources.CVC;
            _notify_icon.Text = "Mark Scan.Net";

            _notify_icon.MouseDoubleClick += notify_icon_MouseDoubleClick;
            System.Windows.Forms.ContextMenuStrip menu = new System.Windows.Forms.ContextMenuStrip();
            menu.Items.Add("Открыть форму", null, notify_icon_show_form);

            menu.Items.Add(new System.Windows.Forms.ToolStripSeparator());
            menu.Items.Add("Выход", null, notify_icon_exit);
            _notify_icon.ContextMenuStrip = menu;
            _notify_icon.Visible = true;
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            _mainWindowsVm.SetWindowHide();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Minimized)
                _mainWindowsVm.SetWindowHide();
        }

        private void image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _mainWindowsVm.ChekUpdate();
        }

        #region Handler eventes icon tray

        /// <summary>
        /// Обработчик двойного клика иконки трея
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notify_icon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _mainWindowsVm.SetWindowShow();
        }
        /// <summary>
        /// Обработчик  меню трея открыть окно главной формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notify_icon_show_form(object sender, EventArgs e)
        {
            _mainWindowsVm.SetWindowShow();
        }
        /// <summary>
        /// Обработчик  меню трея закрыть программу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notify_icon_exit(object sender, EventArgs e)
        {
            _notify_icon.Visible = false;
            App.Current.Shutdown();
        }

        #endregion
    }
}
