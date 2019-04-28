using System;
using System.Collections.Generic;
using System.Text;

namespace Syski.Data
{
    public class Manufacturer
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Model> Models { get; set; }

        public IEnumerable<BIOSModel> BIOSManufacturer { get; set; }

    }
}
