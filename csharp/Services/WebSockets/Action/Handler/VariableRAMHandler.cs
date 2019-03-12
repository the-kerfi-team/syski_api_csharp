using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using csharp.Data;
using System.Net.WebSockets;

namespace csharp.Services.WebSockets.Action.Handler
{
    public class VariableRAMHandler : ActionHandler
    {
        public VariableRAMHandler(IServiceProvider serviceProvider, WebSocket webSocket, Action action) : base(serviceProvider, webSocket, action)
        {
        }

        public override void HandleAction()
        {
            var context = _ServiceProvider.GetService<ApplicationDbContext>();
            var systemUUID = _ServiceProvider.GetService<WebSocketManager>().GetId(_WebSocket);

            var system = context.Systems.Where(u => u.Id == systemUUID).FirstOrDefault();
            if (system != null)
            {
                int freeFromJSON = (int)_Action.properties.SelectToken("free");
                DateTime dateTime = DateTime.Now;

                var systemRAMData = new SystemRAMData()
                {
                    SystemId = system.Id,
                    CollectionDateTime = dateTime,
                    Free = freeFromJSON,
                };
                context.Add(systemRAMData);
                context.SaveChanges();
            }
        }
    }
}
