using Microsoft.Extensions.DependencyInjection;
using Syski.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syski.WebSocket.Services.WebSockets.Actions.Handlers
{
    public class VariableStorageHandler : ActionHandler
    {

        public VariableStorageHandler(Action action, WebSocketConnection webSocketConnection, IServiceProvider serviceProvider) : base(action, webSocketConnection, serviceProvider)
        {
        }

        public override void HandleAction()
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<SyskiDBContext>();
                var systemUUID = serviceProvider.GetService<WebSocketManager>().GetSystemId(webSocketConnection.Id);

                float timeFromJSON = (float) action.properties.SelectToken("time");
                float transfersFromJSON = (float) action.properties.SelectToken("transfers");
                float readsFromJSON = (float) action.properties.SelectToken("reads");
                float writesFromJSON = (float) action.properties.SelectToken("writes");
                float byteReadsFromJSON = (float) action.properties.SelectToken("bytereads");
                float byteWritesFromJSON = (float) action.properties.SelectToken("bytewrites");
                DateTime dateTime = DateTime.Now;

                var systemStorageData = new SystemStorageData()
                {
                    SystemId = systemUUID,
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
