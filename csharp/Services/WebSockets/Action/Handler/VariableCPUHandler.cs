using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using csharp.Data;
using Microsoft.Extensions.DependencyInjection;

namespace csharp.Services.WebSockets.Action.Handler
{
    public class VariableCPUHandler : ActionHandler
    {

        public VariableCPUHandler(IServiceProvider serviceProvider, WebSocketConnection webSocket, Action action) : base(serviceProvider, webSocket, action)
        {
        }

        public override void HandleAction()
        {
            var context = new ApplicationDbContext();
            var systemUUID = _ServiceProvider.GetService<WebSocketManager>().GetId(_WebSocket);

            var system = context.Systems.Where(u => u.Id == systemUUID).FirstOrDefault();
            if (system != null)
            {
                double loadFromJSON = (double) _Action.properties.SelectToken("load");
                int processesFromJSON = (int) _Action.properties.SelectToken("processes");
                DateTime dateTime = DateTime.Now;

                var systemCPUData = new SystemCPUData()
                {
                    SystemId = system.Id,
                    CollectionDateTime = dateTime,
                    Load = loadFromJSON,
                    Processes = processesFromJSON
                };
                context.Add(systemCPUData);
                context.SaveChanges();
            }
        }

    }
}
