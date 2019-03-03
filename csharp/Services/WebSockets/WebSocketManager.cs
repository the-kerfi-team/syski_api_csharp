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

        private Thread _Taskthread;
        private ConcurrentDictionary<Guid, WebSocketData> _WebSockets = new ConcurrentDictionary<Guid, WebSocketData>();

        public WebSocketManager()
        {
            _Taskthread = new Thread(new ThreadStart(RunTasks));
            _Taskthread.Start();
        }

        public void RunTasks()
        {
            while (true)
            {
                foreach (var WebSocket in _WebSockets)
                {
                    int taskCount = WebSocket.Value.countTasks();
                    for (int i = 0; i < taskCount; i++)
                    {
                        ActionTask currentTask = null;
                        try
                        {
                            currentTask = WebSocket.Value.nextTask();
                            if (currentTask.runAtDateTime < DateTime.Now)
                            {
                                string sendAction = JsonConvert.SerializeObject(currentTask.action);
                                currentTask.webSocket.SendAsync(buffer: new ArraySegment<byte>(array: Encoding.ASCII.GetBytes(sendAction), offset: 0, count: sendAction.Length), messageType: WebSocketMessageType.Text, endOfMessage: true, cancellationToken: CancellationToken.None);
                                if (currentTask.repeat)
                                {
                                    currentTask.runAtDateTime = DateTime.Now.AddSeconds(currentTask.delay);
                                    WebSocket.Value.addTask(currentTask);
                                }
                            }
                            else
                            {
                                WebSocket.Value.addTask(currentTask);
                            }
                        }
                        catch (InvalidOperationException)
                        {
                        }
                    }
                }
            }
        }

        public WebSocket GetSocketById(Guid id)
        {
            return (_WebSockets.FirstOrDefault(p => p.Key == id).Value == null ? null : _WebSockets.FirstOrDefault(p => p.Key == id).Value._WebSocket);
        }


        public Guid GetId(WebSocket webSocket)
        {
            return _WebSockets.FirstOrDefault(p => p.Value._WebSocket == webSocket).Key;
        }

        public void AddSocket(Guid id, WebSocket webSocket)
        {
            _WebSockets.TryAdd(id, new WebSocketData(webSocket));
        }

        public async Task RemoveSocket(Guid id)
        {
            WebSocketData webSocket;
            _WebSockets.TryRemove(id, out webSocket);
            if (webSocket != null)
            {
                try
                {
                    await webSocket._WebSocket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure, statusDescription: "Closed by the Server", cancellationToken: CancellationToken.None);
                }
                catch (WebSocketException e)
                {

                }
            }
        }

    }
}
