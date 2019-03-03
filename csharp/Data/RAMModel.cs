using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class RAMModel
    {
        public Guid Id { get; set; }

        public virtual MemoryModel MemoryModel { get; set; }

        public virtual List<SystemRAM> SystemRAMs { get; set; }

        public virtual GPUModel GPUModel { get; set; }
    }
}
