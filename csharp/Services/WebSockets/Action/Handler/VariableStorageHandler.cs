using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using csharp.Data;
using Microsoft.Extensions.DependencyInjection;

namespace csharp.Services.WebSockets.Action.Handler
{
    public class VariableStorageHandler : ActionHandler
    {
        public VariableStorageHandler(IServiceProvider serviceProvider, WebSocket webSocket, Action action) : base(serviceProvider, webSocket, action)
        {
        }

        public override void HandleAction()
        {
            var context = _ServiceProvider.GetService<ApplicationDbContext>();
            var systemUUID = _ServiceProvider.GetService<WebSocketManager>().GetId(_WebSocket);

            var system = context.Systems.Where(u => u.Id == systemUUID).FirstOrDefault();
            if (system != null)
            {
                float timeFromJSON = (float)_Action.properties.SelectToken("time");
                float transfersFromJSON = (float)_Action.properties.SelectToken("transfers");
                float readsFromJSON = (float)_Action.properties.SelectToken("reads");
                float writesFromJSON = (float)_Action.properties.SelectToken("writes");
                float byteReadsFromJSON = (float)_Action.properties.SelectToken("bytereads");
                float byteWritesFromJSON = (float)_Action.properties.SelectToken("bytewrites");
                DateTime dateTime = DateTime.Now;

                var systemStorageData = new SystemStorageData()
                {
                    SystemId = system.Id,
                    CollectionDateTime = dateTime,
                    Time = timeFromJSON,
                    Transfers = transfersFromJSON,
                    Reads = readsFromJSON,
                    Writes = writesFromJSON,
                    ByteReads = byteReadsFromJSON,
                    ByteWrites = byteWritesFromJSON
                };
                context.Add(systemStorageData);
                context.SaveChanges();
            }
        }
    }
}
