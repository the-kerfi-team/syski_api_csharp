using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syski.Data
{
    public class Model
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid? ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public IEnumerable<System> Systems { get; set; }

        public CPUModel CPUModel { get; set; }

        public RAMModel RAMModel { get; set; }

        public GPUModel GPUModel { get; set; }

        public StorageModel StorageModel { get; set; }

        public MotherboardModel MotherboardModel { get; set; }

    }
}
