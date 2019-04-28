using System;
using System.Collections.Generic;

namespace Syski.Data
{
    public class System
    {

        public Guid Id { get; set; }

        public string HostName { get; set; }

        public Guid? ModelId { get; set; }

        public Model Model { get; set; }

        public string Secret { get; set; }

        public DateTime LastUpdated { get; set; }

        public IEnumerable<ApplicationUserSystems> Users { get; set; }

        public IEnumerable<SystemType> SystemTypes { get; set; }

        public IEnumerable<SystemCPU> SystemCPUs { get; set; }

        public IEnumerable<SystemRAM> SystemRAMs { get; set; }

        public IEnumerable<SystemGPU> SystemGPUs { get; set; }

        public IEnumerable<SystemStorage> SystemStorages { get; set; }

        public SystemMotherboard SystemMotherboard { get; set; }

        public SystemBIOS SystemBIOS { get; set; }

        public IEnumerable<SystemOS> SystemOSs { get; set; }

        public IEnumerable<SystemPingData> SystemPingData { get; set; }

        public IEnumerable<SystemCPUData> SystemCPUData { get; set; }

        public IEnumerable<SystemRAMData> SystemRAMData { get; set; }

        public IEnumerable<SystemStorageData> SystemStorageData { get; set; }

        public IEnumerable<SystemRunningProcesses> SystemRunningProcesses { get; set; }

        public IEnumerable<SystemCommand> SystemCommands { get; set; }

    }
}
