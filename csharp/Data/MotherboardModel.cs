using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class MotherboardModel
    {
        public Guid Id { get; set; }

        public Guid ModelId { get; set; }

        public virtual Model Model { get; set; }

        public string Version { get; set; }

        public virtual List<SystemMotherboard> SystemMotherboards { get; set; }
    }
}
