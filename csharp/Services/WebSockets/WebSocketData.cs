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
            List<ActionTask> taskList = new List<ActionTask>();
            taskList.Add(new ActionTask()
            {
                action = ActionFactory.createAction("staticsystem"),
                delay = 86400,
                repeat = true,
                webSocket = _WebSocket,
                runAtDateTime = DateTime.Now
            });
            taskList.Add(new ActionTask()
            {
                action = ActionFactory.createAction("staticcpu"),
                delay = 0,
                repeat = false,
                webSocket = _WebSocket,
                runAtDateTime = DateTime.Now
            });
            taskList.Add(new ActionTask()
            {
                action = ActionFactory.createAction("staticram"),
                delay = 0,
                repeat = false,
                webSocket = _WebSocket,
                runAtDateTime = DateTime.Now
            });
            taskList.Add(new ActionTask()
            {
                action = ActionFactory.createAction("staticos"),
                delay = 21600,
                repeat = true,
                webSocket = _WebSocket,
                runAtDateTime = DateTime.Now
            });
            taskList.Add(new ActionTask()
            {
                action = ActionFactory.createAction("staticgpu"),
                delay = 0,
                repeat = false,
                webSocket = _WebSocket,
                runAtDateTime = DateTime.Now
            });
            taskList.Add(new ActionTask()
            {
                action = ActionFactory.createAction("staticmotherboard"),
                delay = 0,
                repeat = false,
                webSocket = _WebSocket,
                runAtDateTime = DateTime.Now
            });
            taskList.Add(new ActionTask()
            {
                action = ActionFactory.createAction("staticstorage"),
                delay = 21600,
                repeat = false,
                webSocket = _WebSocket,
                runAtDateTime = DateTime.Now
            });
            taskList.Add(new ActionTask()
            {
                action = ActionFactory.createAction("variablecpu"),
                delay = 3,
                repeat = true,
                webSocket = _WebSocket,
                runAtDateTime = DateTime.Now
            });
            taskList.Add(new ActionTask()
            {
                action = ActionFactory.createAction("variableram"),
                delay = 3,
                repeat = true,
                webSocket = _WebSocket,
                runAtDateTime = DateTime.Now
            });
            taskList.Add(new ActionTask()
            {
                action = ActionFactory.createAction("variablestorage"),
                delay = 3,
                repeat = true,
                webSocket = _WebSocket,
                runAtDateTime = DateTime.Now
            });
            taskList.Add(new ActionTask()
            {
                action = ActionFactory.createAction("variablenetwork"),
                delay = 3,
                repeat = true,
                webSocket = _WebSocket,
                runAtDateTime = DateTime.Now
            });

            foreach (ActionTask action in taskList)
            {
                _TaskQueue.Add(action);
            }
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
