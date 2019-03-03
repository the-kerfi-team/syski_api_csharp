using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class SystemModelType
    {
        public Guid SystemId { get; set; }

        public virtual System System { get; set; }
        
        public Guid TypeId { get; set; }

        public virtual Type Type { get; set; }
    }
}
