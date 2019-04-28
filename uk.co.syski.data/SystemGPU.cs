using System;
using System.Collections.Generic;
using System.Text;

namespace Syski.Data
{
    public class SystemGPU
    {

        public Guid SystemId { get; set; }

        public System System { get; set; }

        public Guid GPUModelId { get; set; }

        public GPUModel GPUModel { get; set; }

        public int Slot { get; set; }

        public DateTime LastUpdated { get; set; }

    }
}
