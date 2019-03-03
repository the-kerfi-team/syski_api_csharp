using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class ProcessorModel
    {
        public Guid Id { get; set; }

        public virtual Model Model { get; set; }

        public Guid ArchitectureId { get; set; }

        public virtual Architecture Architecture { get; set; }

        public int ClockSpeed { get; set; }

        public int CoreCount { get; set; }

        public int ThreadCount { get; set; }

        public virtual List<SystemCPU> SystemCPUs { get; set; }

        public virtual GPUModel GPUModel { get; set; }
    }
}
