using Microsoft.Extensions.DependencyInjection;
using Syski.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syski.WebSocket.Services.WebSockets.Actions.Handlers
{
    public class VariableRAMHandler : ActionHandler
    {

        public VariableRAMHandler(Action action, WebSocketConnection webSocketConnection, IServiceProvider serviceProvider) : base(action, webSocketConnection, serviceProvider)
        {
        }

        public override void HandleAction()
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<SyskiDBContext>();
                var systemUUID = serviceProvider.GetService<WebSocketManager>().GetSystemId(webSocketConnection.Id);

                int freeFromJSON = (int) action.properties.SelectToken("free");

                var systemRAMData = new SystemRAMData()
                {
                    SystemId = systemUUID,
                    CollectionDateTime = DateTime.Now,
                    Free = freeFromJSON,
                };
                context.Add(systemRAMData);
                context.SaveChanges();
            }
        }

    }
}
