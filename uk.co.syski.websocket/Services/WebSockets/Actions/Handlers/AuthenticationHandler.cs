using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace Syski.WebSocket.Services.WebSockets.Actions.Handlers
{
    public class AuthenticationHandler : ActionHandler
    {

        // Delay between each command run after authentication
        private static readonly int delayBetweenAction = 100;

        // Commands run after authentication
        private static readonly string[] actions = {
            "staticsystem",
            "staticcpu",
            "staticram",
            "staticgpu",
            "staticstorage",
            "staticmotherboard",
            "staticbios",
            "staticos"
        };

        public AuthenticationHandler(Action action, WebSocketConnection webSocketConnection, IServiceProvider serviceProvider) : base(action, webSocketConnection, serviceProvider)
        {
        }

        public override async void HandleAction()
        {
            webSocketConnection.Authentication = true;
            WebSocketManager websocketManager = serviceProvider.GetService<WebSocketManager>();
            websocketManager.AddSocket(webSocketConnection.Id, webSocketConnection);
            foreach (string action in actions)
            {
                await webSocketConnection.SendAction(action);
                Thread.Sleep(delayBetweenAction);
            }
        }

    }
}
