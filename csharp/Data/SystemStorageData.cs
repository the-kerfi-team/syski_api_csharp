using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class SystemStorageData
    {
        public Guid SystemId { get; set; }

        public System System { get; set; }

        public float Time { get; set; }

        public float Transfers { get; set; }

        public float Reads { get; set; }

        public float Writes { get; set; }

        public float ByteReads { get; set; }

        public float ByteWrites { get; set; }

        public DateTime CollectionDateTime { get; set; }
    }
}
