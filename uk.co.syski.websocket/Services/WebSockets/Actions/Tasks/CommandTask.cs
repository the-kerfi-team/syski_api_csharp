using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Syski.Data;
using System;
using System.Linq;

namespace Syski.WebSocket.Services.WebSockets.Actions.Tasks
{
    public class CommandTask : ActionTask
    {

        public CommandTask(IServiceProvider serviceProvider) : base("command", serviceProvider)
        {
        }

        public override void ExecuteActionTask(WebSocketConnection webSocketConnection)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<SyskiDBContext>();
                var systemUUID = serviceProvider.GetService<WebSocketManager>().GetSystemId(webSocketConnection.Id);
                var command = context.SystemCommands.FirstOrDefault(sc => sc.SystemId.Equals(systemUUID) && sc.ExecutedTime == null);
                if (command != null)
                {
                    command.ExecutedTime = DateTime.Now;
                    context.Update(command);
                    if (command.Properties != null)
                    {
                        JObject properties = JObject.Parse(command.Properties);
                        webSocketConnection.SendAction(command.Action, properties);
                    }
                    else
                    {
                        webSocketConnection.SendAction(command.Action);
                    }
                    context.SaveChanges();
                }
            }
        }

    }
}
