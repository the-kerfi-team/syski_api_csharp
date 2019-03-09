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
        private readonly WebSocketHandler _WebSocketHandler;

        public WebSocketMiddleware(RequestDelegate next, WebSocketHandler WebSocketHandler)
        {
            _next = next;
            _WebSocketHandler = WebSocketHandler;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                await _WebSocketHandler.OnConnected(webSocket);
                await Receive(webSocket, async (result, buffer) =>
                {
                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        try
                        {
                            _WebSocketHandler.ReceiveAsync(webSocket, result, buffer);
                        }
                        catch (WebSocketException wse)
                        {
                            await _WebSocketHandler.OnDisconnected(webSocket);
                        }
                    }
                    else if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await _WebSocketHandler.OnDisconnected(webSocket);
                    }
                });
            }
            else
            {
                await _next.Invoke(context);
            }
        }

        private async Task Receive(WebSocket webSocket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];
            while (webSocket.State == WebSocketState.Open)
            {
                try
                {
                    var result = await webSocket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer), cancellationToken: CancellationToken.None);
                    handleMessage(result, buffer);
                }
                catch (WebSocketException wse)
                {
                    await _WebSocketHandler.OnDisconnected(webSocket);
                }
            }
        }
    }

}
