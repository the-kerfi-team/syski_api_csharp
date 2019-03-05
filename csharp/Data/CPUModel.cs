using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class CPUModel
    {
        public Guid Id { get; set; }

        public Guid ModelId { get; set; }

        public virtual Model Model { get; set; }

        public Guid ArchitectureId { get; set; }

        public virtual Architecture Architecture { get; set; }

        public virtual List<SystemCPU> SystemCPUs { get; set; }

    }
}
