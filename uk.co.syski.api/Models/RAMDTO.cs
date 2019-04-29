using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syski.API.Models
{
    public class RAMDTO
    {

        public Guid Id { get; set; }

        public string ModelName { get; set; }

        public string ManufacturerName { get; set; }

        public long MemoryBytes { get; set; }

    }
}
