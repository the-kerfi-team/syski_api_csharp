using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class InterfaceType
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual List<SystemStorage> SystemStorages { get; set; }

    }
}
