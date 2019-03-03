using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class OperatingSystem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual List<SystemOS> SystemOSs { get; set; }
    }
}
