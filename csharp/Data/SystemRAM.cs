using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class SystemRAM
    {
        public Guid SystemId { get; set; }

        public System System { get; set; }

        public Guid RAMModelId { get; set; }

        public RAMModel RAMModel { get; set; }
    }
}
