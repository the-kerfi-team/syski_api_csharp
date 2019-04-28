using Syski.Data;
using System;

namespace Syski.WebSocket.Services.WebSockets.Actions.Tasks
{
    public abstract class ActionTask
    {

        protected readonly string action;
        protected readonly IServiceProvider serviceProvider;
        protected readonly SyskiDBContext context;

        public ActionTask(string action, IServiceProvider serviceProvider)
        {
            this.action = action;
            this.serviceProvider = serviceProvider;
        }

        public abstract void ExecuteActionTask(WebSocketConnection webSocketConnection);

    }
}
