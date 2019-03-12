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

        public string HostName { get; set; }

        public Guid? ModelId { get; set; }

        public virtual Model Model { get; set; }

        public string Secret { get; set; }

        public DateTime LastUpdated { get; set; }

        public virtual List<ApplicationUserSystems> Users { get; set; }

        public virtual List<SystemType> SystemTypes { get; set; }

        public virtual List<SystemCPU> SystemCPUs { get; set; }

        public virtual List<SystemOS> SystemOSs { get; set; }
         
        public virtual List<SystemRAM> SystemRAMs { get; set; }

        public virtual List<SystemGPU> SystemGPUs { get; set; }

        public virtual List<SystemStorage> SystemStorages { get; set; }

        public virtual SystemMotherboard SystemMotherboard { get; set; }

        public virtual List<SystemCPUData> SystemCPUData { get; set; }

        public virtual List<SystemRAMData> SystemRAMData { get; set; }

        public virtual List<SystemNetworkData> SystemNetworkData { get; set; }

        public virtual List<SystemStorageData> SystemStorageData { get; set; }
    }
}
