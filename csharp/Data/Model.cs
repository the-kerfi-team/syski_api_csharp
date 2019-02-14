using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class Model
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public SystemModel SystemModel { get; set; }
    }
}
