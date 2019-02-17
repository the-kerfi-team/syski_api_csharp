using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class SystemOS
    {
        public Guid SystemId { get; set; }

        public System System { get; set; }

        public Guid OSId { get; set; }

        public OperatingSystem OperatingSystem { get; set; }

        public Guid ArchitectureId { get; set; }

        public Architecture Architecture { get; set; }

        public string Version { get; set; }
    }
}
