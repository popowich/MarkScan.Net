using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MarkScan.Network.JsonWrapers
{
    [DataContract]
    internal class ResultRemainings
    {
        [DataMember(Name = "response")]
        internal ProductionRemainingsItems Response { get; set; }
    }

    [DataContract]
    internal class ProductionRemainingsItems
    {
        [DataMember(Name = "date")]
        internal string Date { get; set; }

        [DataMember(Name = "items")]
        internal ProductionRemainings[] Items;
    }

    [DataContract]
    internal class ProductionRemainings
    {
        [DataMember(Name = "id")]
        internal int Id { get; set; }

        [DataMember(Name = "position")]
        internal int Position { get; set; }

        [DataMember(Name = "fullName")]
        internal string FullName { get; set; }

        [DataMember(Name = "shortName")]
        internal string ShortName { get; set; }

        [DataMember(Name = "alcCode")]
        internal string AlcCode { get; set; }

        [DataMember(Name = "capacity")]
        internal double Capacity { get; set; }

        [DataMember(Name = "alcVolume")]
        internal double AlcVolume { get; set; }

        [DataMember(Name = "egaisQuantity")]
        internal double EgaisQuantity { get; set; }

        [DataMember(Name = "realQuantity")]
        internal double? RealQuantity { get; set; }

        [DataMember(Name = "productVCode")]
        internal int ProductVCode { get; set; }
    }

}
