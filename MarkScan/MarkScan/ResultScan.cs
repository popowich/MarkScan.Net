using System.Runtime.Serialization;

namespace MarkScan
{
    [DataContract]
    public class ResultScan
    {
        [DataMember(Name = "alcCode")]
        public string AlcCode { get; set; }

        [DataMember(Name = "quantity")]
        public int Quantity { get; set; }
    }
}
