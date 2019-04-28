using Newtonsoft.Json;
using System;

namespace Syski.API.Models
{
    public class UserTokenDTO
    {

        public string Id { get; set; }


        public string Email { get; set; }

        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty(PropertyName = "expiry")]
        public DateTime Expiry { get; set; }

    }
}
