using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MarkScan.Network.JsonWrapers
{
    [DataContract]
    public class SendDataResult
    {
        [DataMember(Name = "statusCode")]
        public string StatusCode { get; set; }
    }
}
