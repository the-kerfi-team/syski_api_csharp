using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Models
{
    public class MotherboardDTO
    {
        public Guid Id { get; set; }

        public string ModelName { get; set; }

        public string ManufacturerName { get; set; }

        public string SerialNumber { get; set; }

        public string Version { get; set; }
    }
}
