using System;
using System.Collections.Generic;
using System.Text;

namespace Syski.Data
{
    public class OperatingSystemModel
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<SystemOS> SystemOSs { get; set; }

    }
}
