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

        public virtual System System { get; set; }

        public virtual ProcessorModel ProcessorModel { get; set; }

        public virtual MemoryModel MemoryModel { get; set; }

        public virtual MotherboardModel MotherboardModel { get; set; }
    }
}
