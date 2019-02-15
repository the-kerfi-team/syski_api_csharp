using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class SystemModelType
    {
        public Guid SystemModelId { get; set; }

        public SystemModel SystemModel { get; set; }
        
        public Guid TypeId { get; set; }

        public Type Type { get; set; }
    }
}
