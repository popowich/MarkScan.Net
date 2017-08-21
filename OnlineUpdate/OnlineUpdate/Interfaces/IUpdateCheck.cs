using OnlineUpdate.UpdaterEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineUpdate
{
    public interface IUpdateCheck : IDisposable
    {
        Version CurrentVersion { get; set; }

        Version UpgradeToVersion { get; set; }

        DateTime PublicationDate { get; set; }

        string UpdateDescriptiones { get; set; }

        List<FileUpdate> UpdateFiles { get; }

        bool AllowUpdate { get; }

        event EventHandler<BeginCheckUpdateEventArgs> BeginCheckUpdateEvent;

        event EventHandler EndCheckCurrentFileEvent;

        event EventHandler<EndChekUpdateEventArgs> EndCheckUpdateEvent;

        UpdateDescription CheckUpdates();

        void BeginCheckUpdates();

        void StopCheckUpdates();

    }
}
