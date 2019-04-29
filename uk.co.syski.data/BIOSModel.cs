using System;
using System.Collections.Generic;
using System.Text;

namespace Syski.Data
{
    public class BIOSModel
    {

        public Guid Id { get; set; }

        public Guid? ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public IEnumerable<SystemBIOS> SystemBIOSs { get; set; }

    }
}
