using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class SystemCPU
    {
        public Guid SystemID { get; set; }

        public System System { get; set; }

        public Guid CPUModelId { get; set; }

        public ProcessorModel ProcessorModel { get; set; }
    }
}

