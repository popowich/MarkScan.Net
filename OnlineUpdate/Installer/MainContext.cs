using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Installer
{
    /// <summary>
    /// Реализация основого контекста выполения
    /// </summary>
    public class MainContext : System.Windows.Forms.ApplicationContext
    {
        /// <summary>
        /// Основная форма
        /// </summary>
        private ProcessInstallForm mainForm = null;
        /// <summary>
        /// Инсталятор
        /// </summary>
        private Installer installer = null;

        /// <summary>
        /// Конструктор
        /// </summary>
        public MainContext()
        {
            installer = new Installer();
            installer.ErrorEvent += installer_ErrorEvent;
            installer.EndInstallEvent += installer_EndInstallEvent;
            installer.BeginInstallEvent += installer_BeginInstallEvent;
            installer.InstalledFileEvent += installer_InstalledFileEvent;
            installer.ChangingPhaseOperationEvent += installer_ChangingPhaseOperationEvent;

            installer.LoadInstructiones(AppSettings.CurrDir + "\\" + AppSettings.FileInstrunctiones);

            if (installer.ShowWindowProcessInstall)
            {
                mainForm = new ProcessInstallForm(installer);
                mainForm.FormClosed += mainForm_FormClosed;
                mainForm.Show();
            }

            installer.BeginInstall();

            this.ThreadExit += MainContext_ThreadExit;
        }

        /// <summary>
        /// Обработчик при завершении основного потока
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainContext_ThreadExit(object sender, EventArgs e)
        {
            //Запустить ранее созданные приложения
            installer.RunApplicationStopped();
        }

        /// <summary>
        /// Обработчик при закрытии главной формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainForm_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            mainForm.Dispose();
            mainForm = null;

            if(!installer.IsRunInstalling)
                this.ExitThread();
        }

        /// <summary>
        /// Обработчик начала установки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void installer_BeginInstallEvent(object sender, BeginInstallEventArgs e)
        {
            AppSettings.WriteLogDebug("Установка запущена, файлов: " + e.Count.ToString() + " files");
        }
        /// <summary>
        /// Обработчик окончания установки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void installer_EndInstallEvent(object sender, EventArgs e)
        {
            AppSettings.WriteLogDebug("Установка окончена");
            ((Installer)sender).Dispose();

            if (this.mainForm == null || installer.ShowWindowProcessInstall == false)
                this.ExitThread();
        }
        /// <summary>
        /// Обработчик окончания загрузки текущего файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void installer_InstalledFileEvent(object sender, InstallFileEventArgs e)
        {
            AppSettings.WriteLogDebug("Установка файла: " + e.File.DestFile);    
        }
        /// <summary>
        /// Обработчик обытия изменения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void installer_ChangingPhaseOperationEvent(object sender, ChangingPhaseOperationEventArgs e)
        {
            AppSettings.WriteLogDebug("Выполнение действия: " + e.NamePhase);
        }
        /// <summary>
        /// Обработчик ошибки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void installer_ErrorEvent(object sender, ErrorEventArgs e)
        {
            AppSettings.WriteLogError(e.exept);
        }

    }
}
