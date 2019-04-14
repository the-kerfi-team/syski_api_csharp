using csharp.Services.WebSockets.Action;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using csharp.Data;
using Newtonsoft.Json.Linq;

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

        public async Task OnConnected(WebSocketConnection webSocket)
        {
            webSocket.sendAction("authentication");
        }

        public async Task OnDisconnected(WebSocketConnection webSocketConnection)
        {
            _WebSocketManager.RemoveSocket(webSocketConnection.Id);
            if (webSocketConnection.CloseStatus.HasValue)
            {
                await webSocketConnection.WebSocket.CloseAsync(webSocketConnection.CloseStatus.Value, webSocketConnection.CloseStatusDescription, CancellationToken.None);
            }
        }

        public async Task OnReceiveMessage(WebSocketConnection webSocketConnection)
        {
            try
            {
                byte[] receivePayloadBuffer = new byte[4 * 1024];
                WebSocketReceiveResult webSocketReceiveResult = await webSocketConnection.WebSocket.ReceiveAsync(new ArraySegment<byte>(receivePayloadBuffer), webSocketConnection.GetCancellationToken());
                webSocketConnection.ResetCancelationToken();
                while (webSocketReceiveResult.MessageType != WebSocketMessageType.Close)
                {
                    byte[] result = await ReceiveMessagePayloadAsync(webSocketConnection.WebSocket, webSocketReceiveResult, receivePayloadBuffer);
                    try
                    {
                        Action.Action action = JsonConvert.DeserializeObject<Action.Action>(Encoding.UTF8.GetString(result, 0, result.Length));
                        ActionFactory.createActionHandler(_ServiceProvider, webSocketConnection, action).HandleAction();
                    }
                    catch (JsonReaderException e)
                    {
                        var properties = new JObject {{"message", "Invalid message format sent"}};
                        await webSocketConnection.sendAction("error", properties);
                    }
                    webSocketReceiveResult = await webSocketConnection.WebSocket.ReceiveAsync(new ArraySegment<byte>(receivePayloadBuffer), webSocketConnection.GetCancellationToken());
                    webSocketConnection.ResetCancelationToken();
                }
                webSocketConnection.CloseStatus = webSocketReceiveResult.CloseStatus.Value;
                webSocketConnection.CloseStatusDescription = webSocketReceiveResult.CloseStatusDescription;
            }
            catch (WebSocketException wsex) when (wsex.WebSocketErrorCode == WebSocketError.ConnectionClosedPrematurely)
            {

            }
            catch (OperationCanceledException oce)
            {

            }
        }

        private static async Task<byte[]> ReceiveMessagePayloadAsync(WebSocket webSocket, WebSocketReceiveResult webSocketReceiveResult, byte[] receivePayloadBuffer)
        {
            byte[] messagePayload = null;

            if (webSocketReceiveResult.EndOfMessage)
            {
                messagePayload = new byte[webSocketReceiveResult.Count];
                Array.Copy(receivePayloadBuffer, messagePayload, webSocketReceiveResult.Count);
            }
            else
            {
                using (MemoryStream messagePayloadStream = new MemoryStream())
                {
                    messagePayloadStream.Write(receivePayloadBuffer, 0, webSocketReceiveResult.Count);
                    while (!webSocketReceiveResult.EndOfMessage)
                    {
                        webSocketReceiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(receivePayloadBuffer), CancellationToken.None);
                        messagePayloadStream.Write(receivePayloadBuffer, 0, webSocketReceiveResult.Count);
                    }
                    messagePayload = messagePayloadStream.ToArray();
                }
            }
            return messagePayload;
        }

    }

}

