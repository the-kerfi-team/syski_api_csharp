using Microsoft.Extensions.DependencyInjection;
using Syski.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syski.WebSocket.Services.WebSockets.Actions.Handlers
{
    public class VariablePingHandler : ActionHandler
    {

        public VariablePingHandler(Action action, WebSocketConnection webSocketConnection, IServiceProvider serviceProvider) : base(action, webSocketConnection, serviceProvider)
        {
        }

        public override void HandleAction()
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<SyskiDBContext>();
                var systemUUID = serviceProvider.GetService<WebSocketManager>().GetSystemId(webSocketConnection.Id);

                var pingData = context.SystemPingData.OrderByDescending(i => i.SendPingTime).FirstOrDefault(spd => spd.SystemId.Equals(systemUUID));
                pingData.CollectionDateTime = DateTime.Now;
                context.Update(pingData);
                context.SaveChanges();
            }
        }

    }
}
