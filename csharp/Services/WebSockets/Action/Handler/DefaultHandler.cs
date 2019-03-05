using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace csharp.Services.WebSockets.Action.Handler
{
    public class DefaultHandler : ActionHandler
    {

        public DefaultHandler(IServiceProvider serviceProvider, WebSocket webSocket, Action action) : base(serviceProvider, webSocket, action)
        {
        }

        public override void HandleAction()
        {
            var properties = new JObject();
            properties.Add("message", "Invalid Action Sent");
            this.SendMessageAsync(ActionFactory.createAction("error", properties));
        }

    }
}
