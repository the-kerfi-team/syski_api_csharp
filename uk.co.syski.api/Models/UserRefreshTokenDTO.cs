using Newtonsoft.Json;

namespace Syski.API.Models
{
    public class UserRefreshTokenDTO
    {

        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }

    }
}
