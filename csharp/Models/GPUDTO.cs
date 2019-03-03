using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class GPUDTO
    {
        public Guid Id { get; set; }

        public string ModelName { get; set; }

        public string ManufacturerName { get; set; }

        public string ArchitectureName { get; set; }

        public int ClockSpeed { get; set; }

        public int CoreCount { get; set; }

        public int ThreadCount { get; set; }

        public string MemoryTypeName { get; set; }

        public int MemoryBytes { get; set; }
    }
}
