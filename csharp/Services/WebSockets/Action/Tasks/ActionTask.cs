using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace csharp.Services.WebSockets.Action.Tasks
{
    public class ActionTask
    {

        public Action Action { get; set; }

        public DateTime RunAtDateTime { get; set; }

        public int Delay { get; set; }

        public bool Repeat { get; set; }

    }
}
