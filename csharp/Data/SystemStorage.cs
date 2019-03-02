using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class SystemStorage
    {
        public Guid SystemId { get; set; }

        public System System { get; set; }

        public Guid StorageModelId { get; set; }

        public StorageModel StorageModel { get; set; }
    }
}
