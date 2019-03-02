using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace csharp.Services.WebSockets.Action.Handler
{
    public abstract class ActionHandler
    {

        protected readonly IServiceProvider _ServiceProvider;
        protected readonly WebSocket _WebSocket;
        protected readonly Action _Action;

        public ActionHandler(IServiceProvider serviceProvider, WebSocket webSocket, Action action)
        {
            _WebSocket = webSocket;
            _ServiceProvider = serviceProvider;
            _Action = action;
        }

        protected async Task SendMessageAsync(string message)
        {
            if (_WebSocket.State == WebSocketState.Open)
            {
                await _WebSocket.SendAsync(buffer: new ArraySegment<byte>(array: Encoding.ASCII.GetBytes(message), offset: 0, count: message.Length), messageType: WebSocketMessageType.Text, endOfMessage: true, cancellationToken: CancellationToken.None);
            }
        }

        public abstract void HandleAction();

    }
}
