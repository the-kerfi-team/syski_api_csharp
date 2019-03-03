using csharp.Services.WebSockets.Action;
using csharp.Services.WebSockets.Action.Tasks;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace csharp.Services.WebSockets
{
    public class WebSocketData
    {

        private BlockingCollection<ActionTask> _TaskQueue = new BlockingCollection<ActionTask>();
        public WebSocket _WebSocket { get; }

        public WebSocketData(WebSocket WebSocket)
        {
            _WebSocket = WebSocket;
            addDefaultTasks();
        }

        public void addDefaultTasks()
        {
            var TaskStaticSystemData = new ActionTask()
            {
                action = ActionFactory.createAction("staticsystem"),
                delay = 86400,
                repeat = true,
                webSocket = _WebSocket,
                runAtDateTime = DateTime.Now
            };
            _TaskQueue.Add(TaskStaticSystemData);
        }

        public void addTask(ActionTask task)
        {
            _TaskQueue.Add(task);
        }

        public void clearTasks()
        {
            _TaskQueue = new BlockingCollection<ActionTask>();
        }

        public int countTasks()
        {
            return _TaskQueue.Count();
        }

        public ActionTask nextTask()
        {
            ActionTask action = null;
            _TaskQueue.TryTake(out action);
            return action;
        }

    }
}
