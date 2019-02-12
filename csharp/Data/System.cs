﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class System
    {
        public Guid Id { get; set; }

        public Guid ModelId { get; set; }

        public SystemModel SystemModel { get; set; }

        public string HostName { get; set; }

        public DateTime LastUpdated { get; set; }

        public List<SystemCPU> SystemCPUs { get; set; }
    }
}
