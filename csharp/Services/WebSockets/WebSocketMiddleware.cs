using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace csharp.Services.WebSockets
{
    public class WebSocketMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly WebSocketHandler _webSocketHandler;

        public WebSocketMiddleware(RequestDelegate next, WebSocketHandler webSocketHandler)
        {
            _next = next;
            _webSocketHandler = webSocketHandler;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                WebSocketConnection webSocketConnection = new WebSocketConnection(webSocket);

                await _webSocketHandler.OnConnected(webSocketConnection);

                await _webSocketHandler.OnReceiveMessage(webSocketConnection);

                await _webSocketHandler.OnDisconnected(webSocketConnection);
            }
            else
            {
                await _next.Invoke(context);
            }
        }


    }
}
