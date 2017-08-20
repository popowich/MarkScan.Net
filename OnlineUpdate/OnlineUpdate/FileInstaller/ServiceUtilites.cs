using OnlineUpdate.FileInstaller;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace OnlineUpdate.FileInstaller
{
    /// <summary>
    /// Инструменты работы с сервисами
    /// </summary>
    public static class ServiceUtilites
    {
        /// <summary>
        /// Метод проверят существование службы
        /// </summary>
        /// <param name="_name_service"></param>
        /// <returns></returns>
        public static bool ServiceExistence(string _name_service)
        {
            ServiceController[] all_service = ServiceController.GetServices();
            for (int i = 0; i < all_service.Length; i++)
            {
                if (all_service[i].ServiceName == _name_service)
                {
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// Получить состояние службы 
        /// </summary>
        /// <param name="_name_service"></param>
        /// <returns></returns>
        public static ServiceControllerStatus ServiceState(string _name_service)
        {
            ServiceController[] all_service = ServiceController.GetServices();
            for (int i = 0; i < all_service.Length; i++)
            {
                if (all_service[i].ServiceName == _name_service)
                {
                    return all_service[i].Status;
                }
            }

            return ServiceControllerStatus.Stopped;
        }
        /// <summary>
        /// Метод интсаляции службы Windows
        /// </summary>
        /// <param name="_type"></param>
        public static void InstallService(string _serviceName, string _path, string _type)
        {
            using (TransactedInstaller ti = new TransactedInstaller())
            {
                using (ServiceInstall pi = new ServiceInstall(_serviceName))
                {
                    ti.Installers.Add(pi);

                    string[] cmdline = { string.Format("/assemblypath={0}", _path) };

                    pi.Context = new InstallContext(null, cmdline);
                    if (_type.ToLower() == "/install")
                        pi.Install(new Hashtable());
                    else if (_type.ToLower() == "/uninstall")
                        pi.Uninstall(null);
                    else
                        throw new Exception("Invalid command line");
                }
            }
        }
        /// <summary>
        /// Запустить сервис
        /// </summary>
        /// <param name="_nameServise"></param>
        public static void StartService(string _nameServise)
        {
            ServiceController myService = new ServiceController(_nameServise);
            myService.Start();
            myService.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 60));
        }
        /// <summary>
        /// Остановить сервис
        /// </summary>
        /// <param name="_nameServise"></param>
        public static void StopService(string _nameServise)
        {
            ServiceController myService = new ServiceController(_nameServise);
            myService.Stop();
            myService.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 60));
        }
    }
}
