using Microsoft.Extensions.DependencyInjection;
using Syski.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syski.WebSocket.Services.WebSockets.Actions.Handlers
{
    public class VariableCPUHandler : ActionHandler
    {

        public VariableCPUHandler(Action action, WebSocketConnection webSocketConnection, IServiceProvider serviceProvider) : base(action, webSocketConnection, serviceProvider)
        {
        }

        public override void HandleAction()
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<SyskiDBContext>();
                var systemUUID = serviceProvider.GetService<WebSocketManager>().GetSystemId(webSocketConnection.Id);

                double loadFromJSON = (double) action.properties.SelectToken("load");
                int processesFromJSON = (int) action.properties.SelectToken("processes");

                var systemCPUData = new SystemCPUData()
                {
                    SystemId = systemUUID,
                    CollectionDateTime = DateTime.Now,
                    Load = loadFromJSON,
                    Processes = processesFromJSON
                };
                context.Add(systemCPUData);
                context.SaveChanges();
            }
        }

    }
}
