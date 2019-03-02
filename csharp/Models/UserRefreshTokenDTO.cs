using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Models
{
    public class UserRefreshTokenDTO
    {

        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }

    }
}
