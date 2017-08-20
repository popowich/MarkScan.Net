using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections.Specialized;
using System.Configuration.Install;
using System.ServiceProcess;
using System.Collections;
using System.ComponentModel;

namespace OnlineUpdate.FileInstaller
{
    [RunInstaller(true)]
    public class ServiceInstall : System.Configuration.Install.Installer
    {
        /// <summary>
        /// Имя запускаемой службы
        /// </summary>
        private string serviceName;

        /// <summary>
        /// Конструктор
        /// </summary>
        public ServiceInstall(string _serviceName)
        {
            this.serviceName = _serviceName;
            InitializeComponent();         
        }

        private ServiceInstaller serviceInstaller1;
        private ServiceProcessInstaller serviceProcessInstaller1;

        private System.ComponentModel.Container components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.serviceInstaller1 = new System.ServiceProcess.ServiceInstaller();
            this.serviceProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
            // 
            // serviceInstaller1
            // 
            this.serviceInstaller1.DelayedAutoStart = true;
            this.serviceInstaller1.ServiceName = this.serviceName;
            this.serviceInstaller1.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.serviceInstaller1.DelayedAutoStart = false;

            // 
            // serviceProcessInstaller1
            // 
            this.serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceProcessInstaller1.Password = "";
            this.serviceProcessInstaller1.Username = "";
            // 
            // ServiceInstall
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceInstaller1,
            this.serviceProcessInstaller1});

        }
    }
}
