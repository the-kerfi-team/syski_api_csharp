using System;
using System.Collections.Generic;
using System.Text;

namespace Syski.Data
{
    public class MotherboardModel
    {

        public Guid Id { get; set; }

        public Guid? ModelId { get; set; }

        public Model Model { get; set; }

        public string Version { get; set; }

        public IEnumerable<SystemMotherboard> SystemMotherboards { get; set; }

    }
}
