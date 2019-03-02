using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace csharp.Services.WebSockets.Action.Handler
{
    public class SystemAuthenticationHandler : ActionHandler
    {

        public SystemAuthenticationHandler(IServiceProvider serviceProvider, WebSocket webSocket, Action action) : base(serviceProvider, webSocket, action)
        {

        }

        public override void HandleAction()
        {
            
        }
    }
}
