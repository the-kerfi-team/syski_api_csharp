using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class StorageModel
    {
        public Guid Id;

        public MemoryModel MemoryModel { get; set; }

        public List<SystemStorage> SystemStorages { get; set; }
    }
}
