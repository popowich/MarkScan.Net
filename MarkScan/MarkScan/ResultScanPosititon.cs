
using System.Runtime.Serialization;


namespace MarkScan
{
    [DataContract]
    public class ResultScanPosititon
    {
        [DataMember(Name = "items")]
        public ResultScan[] Positions;
    }
}
