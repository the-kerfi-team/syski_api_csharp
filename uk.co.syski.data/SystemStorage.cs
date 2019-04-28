using System;
using System.Collections.Generic;
using System.Text;

namespace Syski.Data
{
    public class SystemStorage
    {

        public Guid SystemId { get; set; }

        public System System { get; set; }

        public Guid StorageModelId { get; set; }

        public StorageModel StorageModel { get; set; }

        public int Slot { get; set; }

        public Guid? StorageInterfaceId { get; set; }

        public StorageInterfaceType StorageInterface { get; set; }

        public DateTime LastUpdated { get; set; }

    }
}
