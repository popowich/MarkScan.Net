
namespace MarkScan.Models
{
    public class MarkScanWriteOffModel : MarkScanModelBase, IMarkScanModel
    {
        public override string FileDataPath
        {
            get { return AppSettings.CurrDir + "\\WriteOffData.txt"; }
        }

        public MarkScanWriteOffModel(bool newDatay) :
            base(newDatay)
        {

        }

        protected override bool _sendToCvC(ResultScanPosititon resultPositiones)
        {
            return Network.CvcOpenApi.GetClientApi().Writeoff(resultPositiones);
        }
    }

}
