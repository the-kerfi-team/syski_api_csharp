using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class SystemOS
    {
        public Guid SystemId { get; set; }

        public virtual System System { get; set; }

        public Guid OperatingSystemId { get; set; }

        public virtual OperatingSystem OperatingSystem { get; set; }

        public Guid ArchitectureId { get; set; }

        public virtual Architecture Architecture { get; set; }

        public string Version { get; set; }

        public DateTime LastUpdated { get; set; }

    }
}
