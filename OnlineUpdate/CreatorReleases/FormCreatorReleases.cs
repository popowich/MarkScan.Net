using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreatorReleases
{
    public partial class FormCreatorReleases : Form
    {
        /// <summary>
        /// Построитель
        /// </summary>
        private BildRealeas bilderRelease = null;
        /// <summary>
        /// Задачи на выполнении
        /// </summary>
        private List<TaskExecution> tasks = null;
        /// <summary>
        /// Запущен
        /// </summary>
        private bool isRun = true;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public FormCreatorReleases()
        {
            InitializeComponent();

            AppSettings.InitConfiguration();
            AppSettings.ErrorEvent += AppSettings_ErrorEvent;
            tasks = new List<TaskExecution>();

            this.OrderTasks();
        }
        /// <summary>
        /// Обработчик загрузки формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormCreatorReleases_Load(object sender, EventArgs e)
        {
            appSettingsBindingSource.DataSource = AppSettings.Settings;
        }
        /// <summary>
        /// Обработчик перед закрытием формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormCreatorReleases_FormClosing(object sender, FormClosingEventArgs e)
        {
            isRun = false;
            AppSettings.Settings.SaveSetting();
        }
        /// <summary>
        /// Обработчик ошибки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppSettings_ErrorEvent(object sender, System.IO.ErrorEventArgs e)
        {
           this.Invoke(new MethodInvoker(() =>
           {
               this.logRichTextBox.Text += e.GetException().Message + "\n\n";
           }));
        }

        /// <summary>
        /// Открыть форму настроек
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optionesToolStripButton_Click(object sender, EventArgs e)
        {
            OptionesForm optionesForm = new OptionesForm();
            if (optionesForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

            }
        }
     
        /// <summary>
        /// Устанвить значение
        /// </summary>
        /// <param name="_value"></param>
        /// <param name="_mess"></param>
        public void SetValue(int _value, string _mess)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                this.taskProgressBar.Value += _value;
                this.prcTextBox.Text = ((this.taskProgressBar.Value * 100) / this.taskProgressBar.Maximum).ToString() + " %";
                this.fileTextBox.Text = _mess;
            }));
        }

        #region Top panel buttones eventes

        /// <summary>
        /// Подготовится к созданию релиза
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preparationBildtoolStripButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(descriptionesRichTextBox.Text))
            {
                MessageBox.Show("Укажите опсиание релиза", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.bilderRelease != null)
                this.bilderRelease.Dispose();

            bilderRelease = new BildRealeas(AppSettings.Settings.CurrDirSourceFilesApp, true, AppSettings.Settings.CurrVersione, DateTime.Now, descriptionesRichTextBox.Text);
            bilderRelease.CreateTreeRelease();

            sourceFilesListView.Items.Clear();
            sourceFilesListView.Groups.Clear();

            Action<FileRelease, TreeNode> FillFiles = null;
            FillFiles = (FileRelease _root, TreeNode _tr) =>
            {
                ListViewGroup group = new ListViewGroup(_root.SourcePath.Replace(AppSettings.Settings.CurrDirSourceFilesApp, "...\\"));
                sourceFilesListView.Groups.Add(group);

                TreeNode trr = null;
                if(_tr != null)
                   trr = _tr.Nodes.Add(_root.SourcePath.Replace(AppSettings.Settings.CurrDirSourceFilesApp, "...\\"));
                else
                    trr = treeView1.Nodes.Add(_root.SourcePath.Replace(AppSettings.Settings.CurrDirSourceFilesApp, "...\\"));
             
                foreach (var file in _root.ChildFiles)
                {
                    if (!file.IsDirectory)
                    {
                        ListViewItem item = sourceFilesListView.Items.Add(new ListViewItem(new string[] { file.FileNameOriginal, file.SizeKb.ToString(), file.MD5 }, group));
                        item.Checked = true;
                        item.Tag = file;
                    }
                }
                foreach (var dir in _root.ChildFiles)
                {
                    if (dir.IsDirectory)
                        FillFiles(dir,trr);
                }
            }; FillFiles(bilderRelease.RootRelease, null);

            treeView1.ExpandAll();
        }
        /// <summary>
        /// Создать релиз
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bildToolStripButton_ButtonClick(object sender, EventArgs e)
        {
            if (bilderRelease == null || bilderRelease.RootRelease == null)
            {
                MessageBox.Show("Релиз не подготовлен для создания!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bilderRelease.BeginBildReleaseEvent += bilderRelease_BeginBildReleaseEvent;
            bilderRelease.CreatedFileEvent += bilderRelease_CreatedFileEvent;
            bilderRelease.EndBildReleaseEvent += bilderRelease_EndBildReleaseEvent;
            bilderRelease.ErrorEvent += AppSettings_ErrorEvent;

            lock (this.tasks)
            {
                this.tasks.Add(new TaskExecution(bilderRelease));
            }
        }
        /// <summary>
        /// Пубиковать релиз на FTP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void publicationToolStripButton_Click(object sender, EventArgs e)
        {
            if(bilderRelease == null || bilderRelease.IsCreatedSuccessfully == false)
            {
                MessageBox.Show("Релиз не создан, публиковать нечего!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PublicationOnFtp publication = new PublicationOnFtp(AppSettings.Settings.UrlFtp, AppSettings.Settings.PortFtp, AppSettings.Settings.LoginFtp, AppSettings.Settings.PasswordFtp, bilderRelease.RootRelease, AppSettings.Settings.PublicationDirFtp);

            publication.BeginUpLoadEvent += publication_BeginUpLoadEvent;
            publication.EndUpLoadEvent += publication_EndUpLoadEvent;
            publication.UpLoadFileEvent += publication_UpLoadFileEvent;
            publication.ErrorEvent += AppSettings_ErrorEvent;

            lock (this.tasks)
            {
                this.tasks.Add(new TaskExecution(publication));
            }
        }
        /// <summary>
        /// Создать релиз, после чего опубиковать на FTP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void создатьИОпубликоватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bilderRelease == null || bilderRelease.RootRelease == null)
            {
                MessageBox.Show("Релиз не подготовлен для создания!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bilderRelease.BeginBildReleaseEvent += bilderRelease_BeginBildReleaseEvent;
            bilderRelease.CreatedFileEvent += bilderRelease_CreatedFileEvent;
            bilderRelease.EndBildReleaseEvent += bilderRelease_EndBildReleaseEvent;
            bilderRelease.ErrorEvent += AppSettings_ErrorEvent;

            PublicationOnFtp publication = new PublicationOnFtp(AppSettings.Settings.UrlFtp, AppSettings.Settings.PortFtp, AppSettings.Settings.LoginFtp, AppSettings.Settings.PasswordFtp, bilderRelease.RootRelease, AppSettings.Settings.PublicationDirFtp);
            publication.BeginUpLoadEvent += publication_BeginUpLoadEvent;
            publication.EndUpLoadEvent += publication_EndUpLoadEvent;
            publication.UpLoadFileEvent += publication_UpLoadFileEvent;
            publication.ErrorEvent += AppSettings_ErrorEvent;
            
            lock(this.tasks)
            {
                this.tasks.Add(new TaskExecution(publication));
                this.tasks.Add(new TaskExecution(bilderRelease));            
            }
        }

        #endregion

        #region Releas bild eventes

        /// <summary>
        /// Обработчик начала создания релиза
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bilderRelease_BeginBildReleaseEvent(object sender, BeginBildEventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                this.taskProgressBar.Value = 0;
                this.taskProgressBar.Maximum = e.CountFiles;
            }));
        }
        /// <summary>
        /// Обработчик ококнчании операции над единичным файлом
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bilderRelease_CreatedFileEvent(object sender, CreatedFileEventArgs e)
        {
            this.SetValue(1, e.File.FileNameOriginal);
        }
        /// <summary>
        /// Обработчик окончания создания релиза
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bilderRelease_EndBildReleaseEvent(object sender, EventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                bilderRelease.BeginBildReleaseEvent -= bilderRelease_BeginBildReleaseEvent;
                bilderRelease.CreatedFileEvent -= bilderRelease_CreatedFileEvent;
                bilderRelease.EndBildReleaseEvent -= bilderRelease_EndBildReleaseEvent;
                bilderRelease.ErrorEvent -= AppSettings_ErrorEvent;
            }));
        }

        #endregion

        #region Publication releas on FTP

        /// <summary>
        /// Обработчик начала публикации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void publication_BeginUpLoadEvent(object sender, BeginUpLoadEventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                this.taskProgressBar.Value = 0;
                this.taskProgressBar.Maximum = e.Count;
            }));
        }
        /// <summary>
        /// Обработчик публикации одного файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void publication_EndUpLoadEvent(object sender, EventArgs e)
        {
            ((PublicationOnFtp)sender).Dispose();
        }
        /// <summary>
        /// Обработчик завершения публикации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void publication_UpLoadFileEvent(object sender, UpLoadFileEventArgs e)
        {
            this.SetValue(1, e.File.FileNameOriginal);
        }

        #endregion
    }
}
