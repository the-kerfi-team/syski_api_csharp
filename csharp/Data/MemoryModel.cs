using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class MemoryModel
    {
        public Guid Id { get; set; }

        public virtual Model Model { get; set; }

        public Guid MemoryTypeId { get; set; }

        public virtual MemoryType MemoryType { get; set; }

        public int MemoryBytes { get; set; }

        public virtual RAMModel RAMModel { get; set; }

        public virtual StorageModel StorageModel { get; set; }
    }
}
