using System;
using System.Collections.Generic;
using System.Text;

namespace Syski.Data
{
    public class SystemTypeName
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<SystemType> SystemTypes { get; set; }

    }
}
