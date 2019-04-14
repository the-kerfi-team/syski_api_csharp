using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using csharp.Data;
using Microsoft.Extensions.DependencyInjection;

namespace csharp.Services.WebSockets.Action.Handler
{
    public class VariableNetworkHandler : ActionHandler
    {
        public VariableNetworkHandler(IServiceProvider serviceProvider, WebSocketConnection webSocket, Action action) : base(serviceProvider, webSocket, action)
        {
        }

        public override void HandleAction()
        {
            var context = new ApplicationDbContext();
            var systemUUID = _ServiceProvider.GetService<WebSocketManager>().GetId(_WebSocket);

            var system = context.Systems.Where(u => u.Id == systemUUID).FirstOrDefault();
            if (system != null)
            {
                float bandwidthFromJSON = (float)_Action.properties.SelectToken("bandwidth");
                float bytesFromJSON = (float)_Action.properties.SelectToken("bytes");
                float packetsFromJSON = (float)_Action.properties.SelectToken("packets");
                DateTime dateTime = DateTime.Now;

                var systemNetworkData = new SystemNetworkData()
                {
                    SystemId = system.Id,
                    CollectionDateTime = dateTime,
                    Bandwidth = bandwidthFromJSON,
                    Bytes = bytesFromJSON,
                    Packets = packetsFromJSON                    
                };
                context.Add(systemNetworkData);
                context.SaveChanges();
            }
        }
    }
}
