using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class SystemGPU
    {
        public Guid SystemId { get; set; }

        public System System { get; set; }

        public Guid GPUModelId { get; set; }

        public GPUModel GPUModel { get; set; }
    }
}
