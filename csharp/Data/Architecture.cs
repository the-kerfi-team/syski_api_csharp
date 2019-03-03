using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class Architecture
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual List<ProcessorModel> ProcessorModels { get; set; }

        public virtual List<SystemOS> SystemOSs { get; set; }
    }
}
