using System;
using System.Collections.Generic;
using System.Text;

namespace Syski.Data
{
    public class SystemPingData
    {

        public Guid SystemId { get; set; }

        public System System { get; set; }

        public DateTime SendPingTime { get; set; }

        public DateTime CollectionDateTime { get; set; }

    }
}
