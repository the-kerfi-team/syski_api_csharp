using System;
using System.Collections.Generic;
using System.Text;

namespace Syski.Data
{
    public class SystemRAM
    {

        public Guid SystemId { get; set; }

        public System System { get; set; }

        public Guid RAMModelId { get; set; }

        public RAMModel RAMModel { get; set; }

        public int Slot { get; set; }

        public int Speed { get; set; }

        public DateTime LastUpdated { get; set; }

    }
}
