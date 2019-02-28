﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class RAMModel
    {
        public Guid Id { get; set; }

        public MemoryModel MemoryModel { get; set; }

        public List<SystemRAM> SystemRAMs { get; set; }

        public GPUModel GPUModel { get; set; }
    }
}
