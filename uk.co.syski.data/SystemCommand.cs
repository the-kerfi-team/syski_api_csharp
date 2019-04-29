using System;
using System.Collections.Generic;
using System.Text;

namespace Syski.Data
{
    public class SystemCommand
    {

        public Guid SystemId { get; set; }

        public System System { get; set; }

        public string Action { get; set; }

        public string Properties { get; set; }

        public DateTime QueuedTime { get; set; }

        public DateTime? ExecutedTime { get; set; }

    }
}
