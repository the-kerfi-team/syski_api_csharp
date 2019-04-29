using System;
using System.Collections.Generic;
using System.Text;

namespace Syski.Data
{
    public class CPUModel
    {

        public Guid Id { get; set; }

        public Guid? ModelId { get; set; }

        public Model Model { get; set; }

        public Guid? ArchitectureId { get; set; }

        public Architecture Architecture { get; set; }

        public IEnumerable<SystemCPU> SystemCPUs { get; set; }

    }
}
