using System;
using System.Collections.Generic;
using System.Text;

namespace Syski.Data
{
    public class SystemType
    {

        public Guid SystemId { get; set; }

        public System System { get; set; }

        public Guid TypeId { get; set; }

        public SystemTypeName Type { get; set; }

    }
}
