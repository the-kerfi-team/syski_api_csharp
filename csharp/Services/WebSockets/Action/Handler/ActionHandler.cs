using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace csharp.Services.WebSockets.Action.Handler
{
    public abstract class ActionHandler
    {

        protected readonly IServiceProvider _ServiceProvider;
        protected readonly WebSocketConnection _WebSocket;
        protected readonly Action _Action;

        public ActionHandler(IServiceProvider serviceProvider, WebSocketConnection webSocket, Action action)
        {
            _WebSocket = webSocket;
            _ServiceProvider = serviceProvider;
            _Action = action;
        }

        public abstract void HandleAction();

    }
}
