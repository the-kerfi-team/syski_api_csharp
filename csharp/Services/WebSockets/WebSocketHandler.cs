using csharp.Services.WebSockets.Action;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using csharp.Data;

namespace csharp.Services.WebSockets
{
    public class WebSocketHandler
    {

        private readonly IServiceProvider _ServiceProvider;
        private readonly WebSocketManager _WebSocketManager;

        public WebSocketHandler(IServiceProvider serviceProvider)
        {
            _ServiceProvider = serviceProvider;
            _WebSocketManager = serviceProvider.GetService<WebSocketManager>();
        }

        public virtual async Task OnConnected(WebSocket webSocket)
        {
            int maxTries = 3;
            var buffer = new byte[1024 * 4];
            int tries = 0;
            bool authenticated = false;

            Action.Action action = null;
            while (!(authenticated || tries >= maxTries))
            {
                await SendMessageAsync(webSocket, JsonConvert.SerializeObject(ActionFactory.createAction("authentication")));
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                action = JsonConvert.DeserializeObject<Action.Action>(Encoding.UTF8.GetString(buffer, 0, result.Count));
                ActionFactory.createActionHandler(_ServiceProvider, webSocket, action).HandleAction();
                authenticated = (_WebSocketManager.GetId(webSocket) != Guid.Empty);
                tries++;
            }

            if (tries >= maxTries)
            {
                try
                {
                    await webSocket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure, statusDescription: "Failed Authentication", cancellationToken: CancellationToken.None);
                }
                catch (WebSocketException wse)
                {

                }
            }

        }

        public virtual async Task OnDisconnected(WebSocket webSocket)
        {
            await _WebSocketManager.RemoveSocket(_WebSocketManager.GetId(webSocket));
        }

        public async Task SendMessageAsync(WebSocket webSocket, string message)
        {
            if (webSocket.State == WebSocketState.Open)
            {
                await webSocket.SendAsync(buffer: new ArraySegment<byte>(array: Encoding.ASCII.GetBytes(message), offset: 0, count: message.Length), messageType: WebSocketMessageType.Text, endOfMessage: true, cancellationToken: CancellationToken.None);
            }
        }

        public async Task SendMessageAsync(Guid Id, string message)
        {
            await SendMessageAsync(_WebSocketManager.GetSocketById(Id), message);
        }

        public void ReceiveAsync(WebSocket webSocket, WebSocketReceiveResult result, byte[] buffer)
        {
            Action.Action action = JsonConvert.DeserializeObject<Action.Action>(Encoding.UTF8.GetString(buffer, 0, result.Count));
            ActionFactory.createActionHandler(_ServiceProvider, webSocket, action).HandleAction();
        }
    }

}

