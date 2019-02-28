using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class GPUModel
    {
        public Guid Id;

        public ProcessorModel ProcessorModel { get; set; }

        public RAMModel RAMModel { get; set; }

        public List<SystemGPU> SystemGPUs { get; set; }
    }
}
