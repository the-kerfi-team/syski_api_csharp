using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Models
{
    public class SystemDTO
    {
        public Guid Id { get; set; }

        public string ModelName { get; set; }

        public string ManufacturerName { get; set; }

        public string SystemType { get; set; }

        public string HostName { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
