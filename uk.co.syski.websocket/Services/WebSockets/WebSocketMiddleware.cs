using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Syski.WebSocket.Services.WebSockets
{
    public class WebSocketMiddleware
    {

        private readonly RequestDelegate next;
        private readonly IWebSocketHandler webSocketHandler;

        public WebSocketMiddleware(RequestDelegate next, IWebSocketHandler webSocketHandler)
        {
            this.next = next;
            this.webSocketHandler = webSocketHandler;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                WebSocketConnection webSocketConnection = new WebSocketConnection(webSocket);

                await webSocketHandler.OnConnected(webSocketConnection);

                await webSocketHandler.OnReceiveMessage(webSocketConnection);

                await webSocketHandler.OnDisconnected(webSocketConnection);
            }
            else
            {
                await next.Invoke(context);
            }
        }

    }
}
