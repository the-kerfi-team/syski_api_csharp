using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class Model
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid ManufacturerId { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual List<System> Systems { get; set; }

        public virtual StorageModel StorageModel { get; set; }

        public virtual CPUModel ProcessorModel { get; set; }

        public virtual RAMModel RAMModel { get; set; }

        public virtual GPUModel GPUModel { get; set; }

        public virtual MotherboardModel MotherboardModel { get; set; }
    }
}
