using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class MemoryType
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<MemoryModel> MemoryModels { get; set; }
    }
}
