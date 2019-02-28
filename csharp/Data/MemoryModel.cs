using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class MemoryModel
    {
        public Guid Id { get; set; }

        public Model Model { get; set; }

        public Guid MemoryTypeId { get; set; }

        public MemoryType MemoryType { get; set; }

        public int MemoryBytes { get; set; }

        public RAMModel RAMModel { get; set; }       
    }
}
