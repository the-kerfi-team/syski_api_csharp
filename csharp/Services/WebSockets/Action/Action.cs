using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp.Services.WebSockets.Action
{
    public class Action
    {

        public string action { get; set; }

        public JObject properties { get; set; }

    }
}
