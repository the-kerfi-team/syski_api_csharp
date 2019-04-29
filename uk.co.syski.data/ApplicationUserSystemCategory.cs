using System;
using System.Collections.Generic;
using System.Text;

namespace Syski.Data
{
    public class ApplicationUserSystemCategory
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<ApplicationUserSystems> Systems { get; set; }

    }
}
