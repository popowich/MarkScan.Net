
namespace MarkScan.Models
{
    public class MarkScanInventoryModel: MarkScanModelBase, IMarkScanModel
    {
        public override string FileDataPath
        {
            get { return AppSettings.CurrDir + "\\InventoryData.txt"; }
        }

        public MarkScanInventoryModel(bool newDatay) :
            base(newDatay)
        {
  
        }

        protected override bool _sendToCvC(ResultScanPosititon resultPositiones)
        {
            return Network.CvcOpenApi.GetClientApi().Remainings(resultPositiones);
        }
    }


}
