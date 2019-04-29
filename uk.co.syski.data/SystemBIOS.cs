using System;
using System.Collections.Generic;
using System.Text;

namespace Syski.Data
{
    public class SystemBIOS
    {

        public Guid SystemId { get; set; }

        public System System { get; set; }

        public Guid BIOSModelId { get; set; }

        public BIOSModel BIOSModel { get; set; }

        public string Caption { get; set; }

        public string Version { get; set; }

        public string Date { get; set; }

        public DateTime LastUpdated { get; set; }

    }
}
