
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Syski.Data;

namespace Syski.WebSocket.Services.WebSockets.Actions.Tasks
{
    public class DefaultTask : ActionTask
    {

        public DefaultTask(string action, IServiceProvider serviceProvider) : base(action, serviceProvider)
        {
        }

        public override void ExecuteActionTask(WebSocketConnection webSocketConnection)
        {
            webSocketConnection.SendAction(action);
        }

    }
}
