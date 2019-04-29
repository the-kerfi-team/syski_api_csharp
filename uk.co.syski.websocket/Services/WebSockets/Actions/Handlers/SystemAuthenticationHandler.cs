using Microsoft.Extensions.DependencyInjection;
using Syski.Data;
using System;
using System.Linq;

namespace Syski.WebSocket.Services.WebSockets.Actions.Handlers
{
    public class SystemAuthenticationHandler : AuthenticationHandler
    {

        public SystemAuthenticationHandler(Action action, WebSocketConnection webSocketConnection, IServiceProvider serviceProvider) : base(action, webSocketConnection, serviceProvider)
        {
        }

        public override void HandleAction()
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<SyskiDBContext>();
                var system = context.Systems.FirstOrDefault(s => s.Id == Guid.Parse((string) action.properties.SelectToken("system")) && s.Secret == (string) action.properties.SelectToken("secret"));
                if (system != null)
                {
                    WebSocketManager websocketManager = serviceProvider.GetService<WebSocketManager>();
                    websocketManager.SetSystemLink(webSocketConnection.Id, Guid.Parse((string) action.properties.SelectToken("system")));
                    base.HandleAction();
                }
            }
        }

    }
}
