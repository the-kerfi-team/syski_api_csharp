using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Syski.WebSocket.Services.WebSockets.Actions;
using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Syski.WebSocket.Services.WebSockets
{
    public class WebSocketHandler : IWebSocketHandler
    {

        private readonly IServiceProvider serviceProvider;
        private readonly WebSocketManager webSocketManager;

        public WebSocketHandler(IServiceProvider serviceProvider, WebSocketManager webSocketManager)
        {
            this.serviceProvider = serviceProvider;
            this.webSocketManager = webSocketManager;
        }

        public async Task OnConnected(WebSocketConnection webSocketConnection)
        {
            await webSocketConnection.SendAction("authentication");
        }

        public async Task OnDisconnected(WebSocketConnection webSocketConnection)
        {
            webSocketManager.RemoveSocket(webSocketConnection.Id);
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
                        Actions.Action action = JsonConvert.DeserializeObject<Actions.Action>(Encoding.UTF8.GetString(result, 0, result.Length));
                        if (webSocketConnection.Authentication)
                        {
                            ActionFactory.CreateActionHandler(action, webSocketConnection, serviceProvider).HandleAction();
                        }
                        else
                        {
                            ActionFactory.CreateAuthActionHandler(action, webSocketConnection, serviceProvider).HandleAction();
                        }
                    }
                    /*catch (JsonReaderException e)
                    {
                        var properties = new JObject { { "message", "Invalid message format sent" } };
                        await webSocketConnection.sendAction("error", properties);
                    }
                    */
                    catch(Exception e)
                    {
                        if (e is NotImplementedException)
                        {
                            var properties = new JObject { { "message", "This API version does not support this action yet" } };
                            await webSocketConnection.SendAction("error", properties);
                        }
                        else if (e is JsonReaderException)
                        {
                            var properties = new JObject { { "message", "Invalid message format sent" } };
                            await webSocketConnection.SendAction("error", properties);
                        }
                    }
                    webSocketReceiveResult = await webSocketConnection.WebSocket.ReceiveAsync(new ArraySegment<byte>(receivePayloadBuffer), webSocketConnection.GetCancellationToken());
                    webSocketConnection.ResetCancelationToken();
                }
                webSocketConnection.CloseStatus = webSocketReceiveResult.CloseStatus.Value;
                webSocketConnection.CloseStatusDescription = webSocketReceiveResult.CloseStatusDescription;
            }
            /*catch (WebSocketException wsex) when (wsex.WebSocketErrorCode == WebSocketError.ConnectionClosedPrematurely)
            {

            }
            catch (OperationCanceledException oce)
            {

            }
            */
            catch
            {

            }
        }

        private static async Task<byte[]> ReceiveMessagePayloadAsync(System.Net.WebSockets.WebSocket webSocket, WebSocketReceiveResult webSocketReceiveResult, byte[] receivePayloadBuffer)
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
