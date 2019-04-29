using Newtonsoft.Json.Linq;
using System;

namespace Syski.WebSocket.Services.WebSockets.Actions.Handlers
{
    public class DefaultHandler : ActionHandler
    {

        public DefaultHandler(Action action, WebSocketConnection webSocket, IServiceProvider serviceProvider) : base(action, webSocket, serviceProvider)
        {
        }

        public override async void HandleAction()
        {
            JObject properties = new JObject
            {
                { "message", "Invalid Action Sent (" + action.action + ")" }
            };
            await webSocketConnection.SendAction("error", properties);
        }

    }
}
