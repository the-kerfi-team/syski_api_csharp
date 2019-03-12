using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class SystemRAMData
    {

        public Guid SystemId { get; set; }

        public System System { get; set; }

        public int Free { get; set; }

        public DateTime CollectionDateTime { get; set; }

    }
}
