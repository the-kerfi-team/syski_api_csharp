using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class System
    {
        public Guid Id { get; set; }

        public string Secret { get; set; }

        public string HostName { get; set; }

        public DateTime LastUpdated { get; set; }

        public Guid? ModelId { get; set; }

        public Model Model { get; set; }

        public virtual List<SystemModelType> SystemTypes { get; set; }

        public virtual List<SystemCPU> SystemCPUs { get; set; }

        public virtual List<SystemOS> SystemOSs { get; set; }
         
        public virtual List<ApplicationUserSystems> Users { get; set; }

        public virtual List<SystemRAM> SystemRAMs { get; set; }

        public virtual List<SystemGPU> SystemGPUs { get; set; }

        public virtual List<SystemStorage> SystemStorages { get; set; }

        public Guid? MotherboardId { get; set; }

        public virtual MotherboardModel MotherboardModel { get; set; }
    }
}
