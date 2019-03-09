using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class StorageType
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<SystemStorage> SystemStorages { get; set; }

    }
}
