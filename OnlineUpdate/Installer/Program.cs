using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Installer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var prc = System.Diagnostics.Process.GetProcessesByName(System.Reflection.Assembly.GetEntryAssembly().GetName().Name);
            if (prc.Length > 1)
            {
                Application.Exit();
                return;
            }

            if (args.Length > 0)
                AppSettings.FileInstrunctiones = args[0];

            if (string.IsNullOrEmpty(AppSettings.FileInstrunctiones))
            {
                System.Windows.Forms.MessageBox.Show("Не указан файл инструкций обновления", "Внмиание!", System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainContext());
            }
        }

    }
}
