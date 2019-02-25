using csharp.Services.WebSockets.Action;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace csharp.Services.WebSockets
{
    public class WebSocketHandler
    {

        private readonly WebSocketManager _WebSocketManager;
        private readonly Data.ApplicationDbContext _context;

        public WebSocketHandler(WebSocketManager WebSocketManager)
        {
            _WebSocketManager = WebSocketManager;
        }

        public virtual async Task OnConnected(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            int tries = 0;
            bool authenticated = false;

            Action.Action action = null;
            while (!authenticated)
            {
                await SendMessageAsync(webSocket, JsonConvert.SerializeObject(ActionFactory.createAction("authentication")));
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                action = JsonConvert.DeserializeObject<Action.Action>(Encoding.UTF8.GetString(buffer, 0, result.Count));
                authenticated = ActionFactory.createActionHandler(webSocket, null, action).HandleAction();
            }

            if (authenticated)
            {
                if (action.action == "userauthentication")
                {
                    var system = new Data.System();
                    _context.Add(system);
                    _context.SaveChanges();
                    _WebSocketManager.AddSocket(system.Id, webSocket);
                    _WebSocketManager.addTask(new Action.Tasks.ActionTask()
                    {
                        action = ActionFactory.createAction("staticsystem"),
                        delay = 86400,
                        repeat = true,
                        webSocket = webSocket,
                        runAtDateTime = DateTime.Now
                    });
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

        public async Task ReceiveAsync(WebSocket webSocket, WebSocketReceiveResult result, byte[] buffer)
        {
            Action.Action action = JsonConvert.DeserializeObject<Action.Action>(Encoding.UTF8.GetString(buffer, 0, result.Count));
            ActionFactory.createActionHandler(webSocket, _WebSocketManager.GetId(webSocket), action).HandleAction();
        }
    }

}

