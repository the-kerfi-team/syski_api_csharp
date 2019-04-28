using System;
using System.Collections.Generic;
using System.Text;

namespace Syski.Data
{
    public class StorageModel
    {

        public Guid Id { get; set; }

        public Guid? ModelId { get; set; }

        public Model Model { get; set; }

        public long Size { get; set; }

        public IEnumerable<SystemStorage> SystemStorages { get; set; }

    }
}
