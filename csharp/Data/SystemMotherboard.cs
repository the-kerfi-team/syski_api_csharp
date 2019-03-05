using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class SystemMotherboard
    {

        public Guid SystemId { get; set; }

        public virtual System System { get; set; }

        public Guid MotherboardModelId { get; set; }

        public virtual MotherboardModel MotherboardModel { get; set; }

        public DateTime LastUpdated { get; set; }

    }
}
