using System;

namespace Syski.WebSocket.Services.WebSockets.Actions.Handlers
{
    public abstract class ActionHandler
    {

        protected readonly Action action;
        protected readonly WebSocketConnection webSocketConnection;
        protected readonly IServiceProvider serviceProvider;

        public ActionHandler(Action action, WebSocketConnection webSocketConnection, IServiceProvider serviceProvider)
        {
            this.action = action;
            this.webSocketConnection = webSocketConnection;
            this.serviceProvider = serviceProvider;
        }

        public abstract void HandleAction();

    }
}
