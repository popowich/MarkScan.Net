using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Installer
{
    /// <summary>
    /// Реализация формы отображения процесса установки
    /// </summary>
    public partial class ProcessInstallForm : Form
    {
        /// <summary>
        /// Инсталятор
        /// </summary>
        private Installer installer;
        /// <summary>
        /// Завершено с ошибкой
        /// </summary>
        private bool isError;
        /// <summary>
        /// Таймер закрытия окна
        /// </summary>
        private Timer timerClose = null;

        /// <summary>
        /// Конструктор
        /// </summary>
        public ProcessInstallForm(Installer _installer)
        {
            InitializeComponent();

            this.Location = new Point(0, 0);

            this.installer = _installer;
            this.installer.EndInstallEvent += installer_EndInstallEvent;
            this.installer.InstalledFileEvent += installer_InstalledFileEvent;
            this.installer.BeginInstallEvent += installer_BeginInstallEvent;
            this.installer.ErrorEvent += installer_ErrorEvent;
            this.installer.BeginInstallFileEvent += installer_BeginInstallFileEvent;

            descriptionesTextBox.Text = this.installer.UpdateDescriptiones.Replace("\n\r","\r\n"); ;

            this.FormClosing += MainForm_FormClosing;
        }


        /// <summary>
        /// Обработчик отобаржения формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessInstallForm_Shown(object sender, EventArgs e)
        {
            descriptionesTextBox.SelectionLength = 0;
        }
        /// <summary>
        /// Обработчик перед закрытием окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (timerClose != null)
                timerClose.Dispose();

            this.installer.EndInstallEvent -= installer_EndInstallEvent;
            this.installer.InstalledFileEvent -= installer_InstalledFileEvent;
            this.installer.BeginInstallEvent -= installer_BeginInstallEvent;
            this.installer.BeginInstallFileEvent -= installer_BeginInstallFileEvent;
            this.installer.ErrorEvent -= installer_ErrorEvent;
        }

        /// <summary>
        /// Обработчик события начала установки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void installer_BeginInstallEvent(object sender, BeginInstallEventArgs e)
        {
           this.Invoke(new MethodInvoker(() =>
           {
               descStateTextBox.Text = "Выполнение";
               processProgressBar.Maximum = e.Count;
           }));
        }
        /// <summary>
        /// Обработчик начала устновки файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void installer_BeginInstallFileEvent(object sender, InstallFileEventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                installedFileLabel.Text = e.File.NameFile;
            }));
        }
        /// <summary>
        /// Обработчик события установки очередного файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void installer_InstalledFileEvent(object sender, InstallFileEventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                processProgressBar.Value++;
                installedFileLabel.Text = e.File.NameFile;
                progressPrcTextBox.Text = (processProgressBar.Value * 100) / processProgressBar.Maximum + " %";
            }));
        }
        /// <summary>
        /// Обработчик завершения устанвоки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void installer_EndInstallEvent(object sender, EventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                if (!isError)
                {
                    statePictureBox.Image = imageList1.Images[0];
                    descStateTextBox.Text = "Завершено успешно";
                }
                else
                {
                    statePictureBox.Image = imageList1.Images[1];
                    descStateTextBox.Text = "Завершено с ошибкой";
                    descStateTextBox.ForeColor = Color.Red;
                }

                timerClose = new Timer();
                timerClose.Interval = 5000;
                timerClose.Tick += (object _s, EventArgs _e) =>
                {
                    timerClose.Stop();
                    timerClose.Dispose();
                    try
                    {
                        this.Close();
                    }
                    catch { }
                }; timerClose.Start();

            }));
        }
        /// <summary>
        /// Обработчик возникновения ошибки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void installer_ErrorEvent(object sender, ErrorEventArgs e)
        {
            this.isError = true;
        }

    }
}
