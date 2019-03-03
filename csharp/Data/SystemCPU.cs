﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class SystemCPU
    {
        public Guid SystemId { get; set; }

        public virtual System System { get; set; }

        public Guid CPUModelID { get; set; }

        public virtual ProcessorModel ProcessorModel { get; set; }
    }
}
