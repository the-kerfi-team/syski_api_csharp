using csharp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using csharp.Services.WebSockets.Action.Tasks;

namespace csharp.Services.WebSockets.Action.Handler
{
    public class SystemAuthenticationHandler : ActionHandler
    {

        public SystemAuthenticationHandler(IServiceProvider serviceProvider, WebSocket webSocket, Action action) : base(serviceProvider, webSocket, action)
        {

        }

        public override void HandleAction()
        {
            var context = _ServiceProvider.GetService<ApplicationDbContext>();
            var system = context.Systems.Where(s => s.Id == Guid.Parse((string)_Action.properties.SelectToken("system")) && s.Secret == (string)_Action.properties.SelectToken("secret")).FirstOrDefault();
            if (system != null)
            {
                var websocketManager = _ServiceProvider.GetService<WebSocketManager>();
                var previousSocket = websocketManager.GetSocketById(system.Id);
                if (previousSocket != null)
                {
                    websocketManager.RemoveSocket(system.Id);
                }
                websocketManager.AddSocket(system.Id, _WebSocket);
            }
        }
    }
}
