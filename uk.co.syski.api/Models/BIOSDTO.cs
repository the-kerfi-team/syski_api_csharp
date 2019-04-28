using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syski.API.Models
{
    public class BIOSDTO
    {

        public Guid Id { get; set; }

        public string ManufacturerName { get; set; }

        public string Caption { get; set; }

        public string Version { get; set; }

        public string Date { get; set; }

    }
}
