using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class GPUModel
    {
        public Guid Id { get; set; }

        public virtual RAMModel RAMModel { get; set; }

        public virtual List<SystemGPU> SystemGPUs { get; set; }
    }
}
