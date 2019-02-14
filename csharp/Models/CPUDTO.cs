using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Models
{
    public class CPUDTO
    {
        public Guid Id { get; set; }

        public string ModelName { get; set; }

        public string ManufacturerName { get; set; }

        public string ArchitectureName { get; set; }

        public double ClockSpeed { get; set; }

        public int CoreCount { get; set; }

        public int ThreadCount { get; set; }
    }
}
