using Microsoft.Extensions.DependencyInjection;
using Syski.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syski.WebSocket.Services.WebSockets.Actions.Handlers
{
    public class StaticOperatingSystemHandler : ActionHandler
    {

        public StaticOperatingSystemHandler(Action action, WebSocketConnection webSocketConnection, IServiceProvider serviceProvider) : base(action, webSocketConnection, serviceProvider)
        {
        }

        public override void HandleAction()
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<SyskiDBContext>();
                var systemUUID = serviceProvider.GetService<WebSocketManager>().GetSystemId(webSocketConnection.Id);

                string osnameFromJSON = (string) action.properties.SelectToken("name");
                string architectureFromJSON = (string) action.properties.SelectToken("architecture");
                string versionFromJSON = (string) action.properties.SelectToken("version");

                var osName = context.OperatingSystemModels.FirstOrDefault(m => m.Name == osnameFromJSON);
                if (osName == null)
                {
                    osName = new Data.OperatingSystemModel()
                    {
                        Name = osnameFromJSON
                    };
                    context.Add(osName);
                    context.SaveChanges();
                }

                var architecture = context.Architectures.Where(m => m.Name == architectureFromJSON).FirstOrDefault();
                if (architecture == null && architectureFromJSON != null)
                {
                    architecture = new Architecture
                    {
                        Name = architectureFromJSON
                    };
                    context.Add(architecture);
                    context.SaveChanges();
                }

                Guid? architectureId = (architecture != null ? architecture.Id : (Guid?)null);
                var systemOS = context.SystemOSs.FirstOrDefault(m => m.SystemId.Equals(systemUUID));
                if (systemOS == null)
                {
                    systemOS = new SystemOS()
                    {
                        SystemId = systemUUID,
                        OperatingSystemId = osName.Id,
                        ArchitectureId = architectureId,
                        Version = versionFromJSON,
                        LastUpdated = DateTime.Now
                    };
                    context.Add(systemOS);
                    context.SaveChanges();
                }
                else
                {
                    if (!systemOS.OperatingSystemId.Equals(osName.Id))
                    {
                        context.Remove(systemOS);
                        systemOS = new SystemOS()
                        {
                            SystemId = systemUUID,
                            OperatingSystemId = osName.Id,
                            ArchitectureId = architectureId,
                            Version = versionFromJSON,
                            LastUpdated = DateTime.Now
                        };
                        context.Add(systemOS);
                        context.SaveChanges();
                    }
                    else
                    {
                        systemOS.ArchitectureId = architectureId;
                        systemOS.Version = versionFromJSON;
                        systemOS.LastUpdated = DateTime.Now;
                        context.Update(systemOS);
                        context.SaveChanges();
                    }
                }

            }
        }

    }
}
