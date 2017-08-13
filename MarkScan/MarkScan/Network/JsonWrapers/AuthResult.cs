using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MarkScan.Network.JsonWrapers
{
    [DataContract]
    class AuthResult
    {
        [DataMember(Name = "code")]
        public int Code { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "localizedMessage")]
        public string LocalizedMessage { get; set; }
    }
}
