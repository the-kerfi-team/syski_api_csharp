using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace csharp.Services.WebSockets.Action.Handler
{
    public class StaticSystemHandler : ActionHandler
    {

        public StaticSystemHandler(WebSocket webSocket, Guid? systemId, Action action) : base(webSocket, systemId, action)
        {

        }

        public override bool HandleAction()
        {
            throw new NotImplementedException();
        }
    }
}
