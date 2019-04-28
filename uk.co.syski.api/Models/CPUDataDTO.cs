using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syski.API.Models
{
    public class CPUDataDTO
    {

        public double Load { get; set; }

        public int Processes { get; set; }

        public DateTime CollectionDateTime { get; set; }

    }
}
