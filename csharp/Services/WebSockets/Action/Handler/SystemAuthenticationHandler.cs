using csharp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.NodeServices;

namespace csharp.Services.WebSockets.Action.Handler
{
    public class SystemAuthenticationHandler : ActionHandler
    {

        public SystemAuthenticationHandler(IServiceProvider serviceProvider, WebSocketConnection webSocket, Action action) : base(serviceProvider, webSocket, action)
        {

        }

        public override async void HandleAction()
        {
            var context = new ApplicationDbContext();
            var system = context.Systems.FirstOrDefault(s => s.Id == Guid.Parse((string)_Action.properties.SelectToken("system")) && s.Secret == (string)_Action.properties.SelectToken("secret"));
            if (system != null)
            {
                var websocketManager = _ServiceProvider.GetService<WebSocketManager>();
                _WebSocket.Authenticated = true;
                websocketManager.AddSocket(_WebSocket.Id, _WebSocket);
                websocketManager.AddSystemToSocketLink(_WebSocket.Id, system.Id);
                string[] actions = { "staticsystem", "staticcpu", "staticram", "staticos", "staticgpu", "staticmotherboard", "staticstorage", "staticbios" };
                foreach (string action in actions)
                {
                    _WebSocket.sendAction(action);
                    Thread.Sleep(100);
                }
            }
        }
    }
}
