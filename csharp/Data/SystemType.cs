using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class SystemType
    {

        public Guid SystemId { get; set; }

        public virtual System System { get; set; }
        
        public Guid TypeId { get; set; }

        public virtual Type Type { get; set; }

        public DateTime LastUpdated { get; set; }

    }
}
