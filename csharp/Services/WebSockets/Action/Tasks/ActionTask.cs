using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace csharp.Services.WebSockets.Action.Tasks
{
    public class ActionTask
    {

        public Action action { get; set; }

        public WebSocket webSocket { get; set; }

        public DateTime runAtDateTime { get; set; }

        public int delay { get; set; }

        public bool repeat { get; set; }

    }
}
