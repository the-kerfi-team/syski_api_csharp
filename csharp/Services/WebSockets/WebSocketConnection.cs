using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using csharp.Services.WebSockets.Action;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace csharp.Services.WebSockets
{
    public class WebSocketConnection
    {

        public Guid Id { get; } = Guid.NewGuid();

        public WebSocket WebSocket { get; }

        public bool Authenticated { get; set; }

        public WebSocketCloseStatus? CloseStatus { set; get; } = null;

        public string CloseStatusDescription { set; get; } = null;

        public CancellationTokenSource CancellationTokenSource;

        public WebSocketConnection(WebSocket webSocket)
        {
            WebSocket = webSocket;
            CancellationTokenSource = new CancellationTokenSource(60000);
        }

        public void ResetCancelationToken()
        {
            CancellationTokenSource.CancelAfter(10000);
        }

        public CancellationToken GetCancellationToken()
        {
            return CancellationTokenSource.Token;
        }

        public async Task sendAction(String actionName)
        {
            await sendMessage(JsonConvert.SerializeObject(ActionFactory.createAction(actionName)));
        }

        public async Task sendAction(String actionName, JObject actionProperties)
        {
            await sendMessage(JsonConvert.SerializeObject(ActionFactory.createAction(actionName, actionProperties)));
        }

        private async Task sendMessage(String message)
        {
            if (WebSocket.State == WebSocketState.Open)
            {
                await WebSocket.SendAsync(buffer: new ArraySegment<byte>(array: Encoding.ASCII.GetBytes(message), offset: 0, count: message.Length), messageType: WebSocketMessageType.Text, endOfMessage: true, cancellationToken: CancellationToken.None);
            }
        }

    }
}
