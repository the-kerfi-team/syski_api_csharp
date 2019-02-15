using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class SystemModel
    {
        public Guid Id { get; set; }

        public Model Model { get; set; }

        public List<SystemModelType> SystemModelTypes { get; set; }

        public List<System> Systems { get; set; }
    }
}
