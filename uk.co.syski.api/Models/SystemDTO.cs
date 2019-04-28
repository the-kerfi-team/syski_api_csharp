using Syski.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syski.API.Models
{
    public class SystemDTO
    {

        public Guid Id { get; set; }

        public string HostName { get; set; }

        public string ModelName { get; set; }

        public string ManufacturerName { get; set; }

        public List<string> SystemTypes { get; set; }

        public bool Online { get; set; }

        public double Ping { get; set; }

        public DateTime LastUpdated { get; set; }

    }
}
