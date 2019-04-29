using System;
using System.Collections.Generic;

namespace Syski.Data
{
    public class Architecture
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<CPUModel> CPUModels { get; set; }

        public IEnumerable<SystemOS> SystemOSs { get; set; }

    }
}
