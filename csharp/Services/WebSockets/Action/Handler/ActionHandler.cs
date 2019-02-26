using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace csharp.Services.WebSockets.Action.Handler
{
    public abstract class ActionHandler
    {

        protected readonly IServiceProvider _ServiceProvider;
        protected readonly WebSocket _WebSocket;
        protected readonly Action _Action;

        public ActionHandler(IServiceProvider serviceProvider, WebSocket webSocket, Action action)
        {
            _WebSocket = webSocket;
            _ServiceProvider = serviceProvider;
            _Action = action;
        }

        public abstract void HandleAction();

    }
}
