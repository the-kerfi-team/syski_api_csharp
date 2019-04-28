using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Syski.WebSocket.Services.WebSockets.Actions;

namespace Syski.WebSocket.Services.WebSockets
{
    public class WebSocketConnection
    {

        public Guid Id { get; } = Guid.NewGuid();

        public System.Net.WebSockets.WebSocket WebSocket { get; }

        public WebSocketCloseStatus? CloseStatus { get; set; } = null;

        public bool Authentication { get; set; } = false;

        public string CloseStatusDescription { get; set; } = null;

        public CancellationTokenSource CancellationTokenSource;

        public WebSocketConnection(System.Net.WebSockets.WebSocket webSocket)
        {
            WebSocket = webSocket;
            CancellationTokenSource = new CancellationTokenSource(60000);
        }

        public void ResetCancelationToken()
        {
            CancellationTokenSource.CancelAfter(20000);
        }

        public CancellationToken GetCancellationToken()
        {
            return CancellationTokenSource.Token;
        }

        public async Task SendAction(String actionName)
        {
            await SendMessage(JsonConvert.SerializeObject(ActionFactory.CreateAction(actionName)));
        }

        public async Task SendAction(String actionName, JObject actionProperties)
        {
            await SendMessage(JsonConvert.SerializeObject(ActionFactory.CreateAction(actionName, actionProperties)));
        }

        private async Task SendMessage(String message)
        {
            if (WebSocket.State == WebSocketState.Open)
            {
                await WebSocket.SendAsync(buffer: new ArraySegment<byte>(array: Encoding.ASCII.GetBytes(message), offset: 0, count: message.Length), messageType: WebSocketMessageType.Text, endOfMessage: true, cancellationToken: CancellationToken.None);
            }
        }

    }
}