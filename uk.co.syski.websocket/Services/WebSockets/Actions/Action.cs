using Newtonsoft.Json.Linq;

namespace Syski.WebSocket.Services.WebSockets.Actions
{
    public class Action
    {

        public string action { get; set; }

        public JObject properties { get; set; }

    }
}
