using System;
using System.Collections.Generic;
using System.Text;

namespace Syski.Data
{
    public class SystemMotherboard
    {

        public Guid SystemId { get; set; }

        public System System { get; set; }

        public Guid MotherboardModelId { get; set; }

        public MotherboardModel MotherboardModel { get; set; }

        public DateTime LastUpdated { get; set; }

    }
}
