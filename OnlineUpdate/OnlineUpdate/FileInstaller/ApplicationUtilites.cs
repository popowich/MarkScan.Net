using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace OnlineUpdate.FileInstaller
{
    public class ApplicationUtilites
    {
        /// <summary>
        /// Приложение запущено
        /// </summary>
        /// <param name="_path"></param>
        /// <returns></returns>
        public static bool ExistRunApplication(string _path)
        {
            Process[] prcs = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(_path));
            if (prcs.Length == 0)
            {
                prcs = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(_path) + ".vshost");
            }

            return prcs.Length > 0;
        }
        /// <summary>
        /// Запустить приложение 
        /// </summary>
        /// <param name="_path"></param>
        public static void StartApplication(string _path, string _argumentes)
        {
            //Запуск процесса инсталяции
            System.Diagnostics.ProcessStartInfo processInfo = new System.Diagnostics.ProcessStartInfo();
            processInfo.FileName = _path;

            processInfo.Arguments = _argumentes;
            System.Diagnostics.Process prc_instal = new System.Diagnostics.Process();
            prc_instal.StartInfo = processInfo;
            prc_instal.Start();
        }
        /// <summary>
        /// Останвоиь запущенное приложение
        /// </summary>
        /// <param name="_path"></param>
        public static void StopApplication(string _path)
        {
            Process[] prcs = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(_path));
            foreach (Process prc in prcs)
            {
                prc.Kill();
            }
            prcs = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(_path) + ".vshost");
            foreach (Process prc in prcs)
            {
                prc.Kill();
            }
        }
    }
}
