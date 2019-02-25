using csharp.Services.WebSockets.Action.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace csharp.Services.WebSockets
{
    public class WebSocketManager
    {

        private Thread _thread;
        private BlockingCollection<ActionTask> _TaskQueue = new BlockingCollection<ActionTask>();
        private ConcurrentDictionary<Guid, WebSocket> _WebSockets = new ConcurrentDictionary<Guid, WebSocket>();

        public WebSocketManager()
        {
            _thread = new Thread(new ThreadStart(Run));
            _thread.Start();
        }

        public void addTask(ActionTask task)
        {
            _TaskQueue.Add(task);
        }

        public void Run()
        {
            while (true)
            {
                ActionTask currentTask = null;
                try
                {
                    _TaskQueue.Take();
                    if (currentTask.runAtDateTime > DateTime.Now)
                    {
                        string sendAction = JsonConvert.SerializeObject(currentTask.action);
                        currentTask.webSocket.SendAsync(buffer: new ArraySegment<byte>(array: Encoding.ASCII.GetBytes(sendAction), offset: 0, count: sendAction.Length), messageType: WebSocketMessageType.Text, endOfMessage: true, cancellationToken: CancellationToken.None);
                        if (currentTask.repeat)
                        {
                            currentTask.runAtDateTime = DateTime.Now.AddMilliseconds(currentTask.delay);
                            _TaskQueue.Add(currentTask);
                        }
                    }
                    else
                    {
                        _TaskQueue.Add(currentTask);
                    }
                }
                catch (InvalidOperationException)
                {
                }
            }
        }

        public WebSocket GetSocketById(Guid id)
        {
            return _WebSockets.FirstOrDefault(p => p.Key == id).Value;
        }


        public Guid GetId(WebSocket webSocket)
        {
            return _WebSockets.FirstOrDefault(p => p.Value == webSocket).Key;
        }

        public void AddSocket(Guid id, WebSocket webSocket)
        {
            _WebSockets.TryAdd(id, webSocket);
        }

        public async Task RemoveSocket(Guid id)
        {
            WebSocket webSocket;
            _WebSockets.TryRemove(id, out webSocket);
            await webSocket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure, statusDescription: "Closed by the Server", cancellationToken: CancellationToken.None);
        }

    }
}
