using System;
using System.Collections.Generic;
using System.Text;

namespace Syski.Data
{
    public class SystemOS
    {

        public Guid SystemId { get; set; }

        public System System { get; set; }

        public Guid OperatingSystemId { get; set; }

        public OperatingSystemModel OperatingSystem { get; set; }

        public Guid? ArchitectureId { get; set; }

        public Architecture Architecture { get; set; }

        public string Version { get; set; }

        public DateTime LastUpdated { get; set; }

    }
}
