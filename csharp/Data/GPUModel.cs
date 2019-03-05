using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class GPUModel
    {

        public Guid Id { get; set; }

        public Guid ModelId { get; set; }

        public virtual Model Model { get; set; }

        public virtual List<SystemGPU> SystemGPUs { get; set; }

    }
}
