using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class SystemType
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<SystemModel> SystemModels { get; set; }
    }
}
