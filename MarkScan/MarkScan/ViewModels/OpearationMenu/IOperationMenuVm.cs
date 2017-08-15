
using MarkScan.Pages;

namespace MarkScan.ViewModels
{
    public interface IOperationMenuVm
    {
        void GoToMainMenuPage();
        void GoToMarkScanNew();
        void GoToMarkScanСontinue();
        void SendDatatoCvC();
        void TestConnect();
        void SetPage(OperationMenuPage operationMenuPage);
    }
}
