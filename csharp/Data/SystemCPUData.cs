﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class SystemCPUData
    {

        public Guid SystemId { get; set; }

        public System System { get; set; }

        public double Load { get; set; }

        public int Processes { get; set; }

        public DateTime CollectionDateTime { get; set; }

    }
}
