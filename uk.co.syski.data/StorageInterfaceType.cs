using System;
using System.Collections.Generic;
using System.Text;

namespace Syski.Data
{
    public class StorageInterfaceType
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<SystemStorage> SystemStorages { get; set; }

    }
}
