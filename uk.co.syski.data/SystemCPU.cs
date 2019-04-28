using System;
using System.Collections.Generic;
using System.Text;

namespace Syski.Data
{
    public class SystemCPU
    {

        public Guid SystemId { get; set; }

        public System System { get; set; }

        public Guid CPUModelID { get; set; }

        public CPUModel CPUModel { get; set; }

        public int Slot { get; set; }

        public int ClockSpeed { get; set; }

        public int CoreCount { get; set; }

        public int ThreadCount { get; set; }

        public DateTime LastUpdated { get; set; }

    }
}
