using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Models
{
    public class RAMDTO
    {
        public Guid Id { get; set; }

        public string ModelName { get; set; }

        public string ManufacturerName { get; set; }

        public string MemoryTypeName { get; set; }

        public int MemoryBytes { get; set; }
    }
}
