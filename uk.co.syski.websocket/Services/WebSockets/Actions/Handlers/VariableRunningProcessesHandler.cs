using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Syski.Data;
using System;

namespace Syski.WebSocket.Services.WebSockets.Actions.Handlers
{
    public class VariableRunningProcessesHandler : ActionHandler
    {

        public VariableRunningProcessesHandler(Action action, WebSocketConnection webSocketConnection, IServiceProvider serviceProvider) : base(action, webSocketConnection, serviceProvider)
        {
        }

        public override void HandleAction()
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<SyskiDBContext>();
                var systemUUID = serviceProvider.GetService<WebSocketManager>().GetSystemId(webSocketConnection.Id);
                DateTime lastUpdated = DateTime.Now;
                JArray processesArray = (JArray) action.properties.SelectToken("processes");

                foreach (JToken process in processesArray)
                {
                    int idFromJSON = (int) process.SelectToken("id");
                    string nameFromJSON = (string) process.SelectToken("name");
                    long memsizetimeFromJSON = (long) process.SelectToken("memsize");
                    long kerneltimeFromJSON = (long) process.SelectToken("kerneltime");
                    string pathFromJSON = (string) process.SelectToken("path");
                    int threadsFromJSON = (int) process.SelectToken("threads");
                    long uptimeFromJSON = (long) process.SelectToken("uptime");
                    int parentidFromJSON = (int) process.SelectToken("parentid");

                    var systemProcess = new SystemRunningProcesses()
                    {
                        SystemId = systemUUID,
                        Id = idFromJSON,
                        Name = nameFromJSON,
                        MemSize = memsizetimeFromJSON,
                        KernelTime = kerneltimeFromJSON,
                        Path = pathFromJSON,
                        Threads = threadsFromJSON,
                        UpTime = uptimeFromJSON,
                        ParentId = parentidFromJSON,
                        CollectionDateTime = lastUpdated
                    };
                    context.Add(systemProcess);
                }
                context.SaveChanges();
            }
        }

    }
}
