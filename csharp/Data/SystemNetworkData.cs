using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class SystemNetworkData
    {
        public Guid SystemId { get; set; }

        public System System { get; set; }

        public float Bandwidth { get; set; }

        public float Bytes { get; set; }

        public float Packets { get; set; }

        public DateTime CollectionDateTime { get; set; }
    }
}
