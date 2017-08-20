using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreatorReleases
{
    public partial class OptionesForm : Form
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        public OptionesForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Обработчик создания формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionesForm_Load(object sender, EventArgs e)
        {
            appSettingsBindingSource.DataSource = AppSettings.Settings;
        }
        /// <summary>
        /// Обработчик кнопки примененения настроек
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void acceptToolStripButton_Click(object sender, EventArgs e)
        {
            this.ValidateChildren();

            AppSettings.Settings.SaveSetting();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        /// <summary>
        /// Отменить изменения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelToolStripButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        /// <summary>
        /// Обработчик кнопки формы выбора пути хранилища релизов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectBildReleasesDirButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbdForm = new FolderBrowserDialog();
            if (fbdForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bildReleasesDirTextBox.Text = fbdForm.SelectedPath;
            }
        }
        /// <summary>
        /// обработчик кнопки открытия формы выбора источника файлов релиза
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectcurrDirProducedyAppButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbdForm = new FolderBrowserDialog();
            if (fbdForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                currDirProducedyAppTextBox.Text = fbdForm.SelectedPath;
            }
        }

        private void urlFtpTextBox_Validating(object sender, CancelEventArgs e)
        {
            urlFtpTextBox.Text = urlFtpTextBox.Text.ToLower().Replace("ftp://", "");
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            this.ValidateChildren();

            try
            {
                FtpClient ftp = new FtpClient(AppSettings.Settings.UrlFtp, AppSettings.Settings.PortFtp, AppSettings.Settings.LoginFtp, AppSettings.Settings.PasswordFtp);
                ftp.TestConnect();

                MessageBox.Show("Соединение создано успешно!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ExceptionService.Except(ex);
            }

        }
    }
}
