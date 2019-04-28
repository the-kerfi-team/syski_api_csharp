using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syski.API.Models
{
    public class KillProcessDTO
    {

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

    }
}
