using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class StorageDTO
    {
        public Guid Id { get; set; }

        public string ModelName { get; set; }

        public string ManufacturerName { get; set; }

        public string MemoryTypeName { get; set; }

        public int MemoryBytes { get; set; }
    }
}
