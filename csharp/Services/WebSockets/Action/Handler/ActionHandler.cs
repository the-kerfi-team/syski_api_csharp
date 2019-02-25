using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace csharp.Services.WebSockets.Action.Handler
{
    public abstract class ActionHandler
    {

        protected readonly WebSocket _WebSocket;
        protected readonly Guid? _SystemId;
        protected readonly Action _Action;

        public ActionHandler(WebSocket webSocket, Guid? systemId, Action action)
        {
            _WebSocket = webSocket;
            _SystemId = systemId;
            _Action = action;
        }

        public abstract bool HandleAction();

    }
}
