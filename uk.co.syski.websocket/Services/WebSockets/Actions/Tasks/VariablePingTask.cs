using System;
using Microsoft.Extensions.DependencyInjection;
using Syski.Data;

namespace Syski.WebSocket.Services.WebSockets.Actions.Tasks
{
    public class VariablePingTask : ActionTask
    {

        public VariablePingTask(IServiceProvider serviceProvider) : base("variableping", serviceProvider)
        {
        }

        public override void ExecuteActionTask(WebSocketConnection webSocketConnection)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<SyskiDBContext>();
                var systemUUID = serviceProvider.GetService<WebSocketManager>().GetSystemId(webSocketConnection.Id);
                context.Add(new SystemPingData
                {
                    SystemId = systemUUID,
                    SendPingTime = DateTime.Now
                });
                context.SaveChanges();
                webSocketConnection.SendAction(action);
            }
        }

    }
}
