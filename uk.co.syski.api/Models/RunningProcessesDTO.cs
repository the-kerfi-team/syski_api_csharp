using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syski.API.Models
{
    public class RunningProcessesDTO
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public long MemSize { get; set; }

        public long KernelTime { get; set; }

        public string Path { get; set; }

        public int Threads { get; set; }

        public long UpTime { get; set; }

        public int ParentId { get; set; }

        public DateTime CollectionDateTime { get; set; }

    }
}
