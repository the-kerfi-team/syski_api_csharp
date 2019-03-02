using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class ProcessorModel
    {
        public Guid Id { get; set; }

        public Model Model { get; set; }

        public Guid ArchitectureId { get; set; }

        public virtual Architecture Architecture { get; set; }

        public int ClockSpeed { get; set; }

        public int CoreCount { get; set; }

        public int ThreadCount { get; set; }

        public List<SystemCPU> SystemCPUs { get; set; }

        public GPUModel GPUModel { get; set; }
    }
}
