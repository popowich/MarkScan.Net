using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkScan.ViewModels
{
    public class AppUpdateDescriptopnVm
    {
        private Pages.AppUpdateDescriptionPage _myPage;
        private OnlineUpdate.UpdateDescription _descriptopnUpdate;

        public AppUpdateDescriptopnVm(OnlineUpdate.UpdateDescription descriptopnUpdate)
        {
            _descriptopnUpdate = descriptopnUpdate;
        }

        public void SetPage(Pages.AppUpdateDescriptionPage myPage)
        {
            _myPage = myPage;

            _myPage.titleUpdateLb.Content = "Доступно обновление приложения " + _descriptopnUpdate.UpgradeToVersion;
            _myPage.decriptUpdateLb.Content = _descriptopnUpdate.UpdateDescriptiones;
        }

        public void GoToUpdate()
        {
            MarkScan.Updater.UpdateService.GetService().SatrtUpate();
        }

        public void CancelUpdate()
        {
            App._mainWindowsVm._generalFrame.GoBack();
        }

    }
}
