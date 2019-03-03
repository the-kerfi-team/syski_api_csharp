using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Data
{
    public class Manufacturer
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual List<Model> Models { get; set; }
    }
}
