using OnlineUpdate.FileInstaller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineUpdate
{
    public interface IUpdateInstall : IDisposable
    {
         event EventHandler<BeginPreparingInstallationEventArgs> BeginPreparingInstallationEvent;

        event EventHandler<EndPreparingInstallationEventArgs> EndPreparingInstallationEvent;

        void InstallFiles();
    }
}
