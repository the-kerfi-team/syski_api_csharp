using System;
using System.Collections.Generic;
using System.Text;

namespace Syski.Data
{
    public class GPUModel
    {

        public Guid Id { get; set; }

        public Guid? ModelId { get; set; }

        public Model Model { get; set; }

        public IEnumerable<SystemGPU> SystemGPUs { get; set; }

    }
}
