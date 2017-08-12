using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MarkScan.Network.JsonWrapers
{
    [DataContract]
    class ResponseBase<T>
    {
        [DataMember(Name = "response")]
        public T Response { get; set; }

        [DataMember(Name = "version")]
        public string Version { get; set; }
    }
}
