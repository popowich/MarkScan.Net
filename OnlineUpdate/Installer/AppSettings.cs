using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Installer
{
    public static class AppSettings
    {
        /// <summary>
        /// Текущая дирректория
        /// </summary>
        public static string CurrDir { get; set; }
        /// <summary>
        /// Путь к файлу инструкций
        /// </summary>
        public static string FileInstrunctiones { get; set; }
        /// <summary>
        /// Путь к лог файлу
        /// </summary>
        public static string LogFile { get; set; }

        /// <summary>
        /// Статический конструктор
        /// </summary>
        static AppSettings()
        {
            CurrDir = System.Windows.Forms.Application.ExecutablePath.Remove(System.Windows.Forms.Application.ExecutablePath.LastIndexOf('\\'));
        }

        /// <summary>
        /// Записать лог
        /// </summary>
        /// <param name="_ex"></param>
        public static void WriteLogError(Exception _ex)
        {
            if (!string.IsNullOrEmpty(Installer.LogFile))
            {
                StreamWriter write = null;
                try
                {
                    write = new StreamWriter(Installer.LogFile, true, Encoding.UTF8);
                    write.WriteLine(DateTime.Now.ToString() + " - UpdateInstaller Error: " + _ex.Message);
                    if (_ex.InnerException != null)
                    {
                        write.WriteLine("                    ->  " + _ex.InnerException.Message);
                        if (_ex.InnerException.InnerException != null)
                        {
                            write.WriteLine("                    -> " + _ex.InnerException.InnerException.Message);
                            if (_ex.InnerException.InnerException.InnerException != null)
                            {
                                write.WriteLine("                      -> " + _ex.InnerException.InnerException.InnerException.Message);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Ошибка записи лога: " + ex.Message, "UpdateInstaller", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                finally
                {
                    if (write != null)
                        write.Close();
                }
            }
        }
        /// <summary>
        /// Записать лог
        /// </summary>
        /// <param name="_ex"></param>
        public static void WriteLogDebug(string _text)
        {
            if (!string.IsNullOrEmpty(Installer.LogFile))
            {
                StreamWriter write = null;
                try
                {
                    write = new StreamWriter(Installer.LogFile, true, Encoding.UTF8);
                    write.WriteLine(DateTime.Now.ToString() + " - UpdateInstaller Debug: " + _text);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Ошибка записи лога: " + ex.Message, "UpdateInstaller", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                finally
                {
                    if (write != null)
                        write.Close();
                }
            }
        }
    }
}
