using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using csharp.Data;
using Microsoft.Extensions.DependencyInjection;

namespace csharp.Services.WebSockets.Action.Handler
{
    public class StaticOperatingSystemHandler : ActionHandler
    {

        public StaticOperatingSystemHandler(IServiceProvider serviceProvider, WebSocket webSocket, Action action) : base(serviceProvider, webSocket, action)
        {
        }

        public override void HandleAction()
        {
            var context = _ServiceProvider.GetService<ApplicationDbContext>();
            var systemUUID = _ServiceProvider.GetService<WebSocketManager>().GetId(_WebSocket);

            var system = context.Systems.Where(u => u.Id == systemUUID).FirstOrDefault();
            if (system != null)
            {
                string osnameFromJSON = (string)_Action.properties.SelectToken("name");
                string architectureFromJSON = (string)_Action.properties.SelectToken("architecture");
                string versionFromJSON = (string)_Action.properties.SelectToken("version");

                var osname = context.OperatingSystems.Where(m => m.Name == osnameFromJSON).FirstOrDefault();
                if (osname == null)
                {
                    osname = new Data.OperatingSystem()
                    {
                        Name = osnameFromJSON
                    };
                    context.Add(osname);
                    context.SaveChanges();
                }

                var architecture = context.Architectures.Where(m => m.Name == architectureFromJSON).FirstOrDefault();
                if (architecture == null)
                {
                    architecture = new Architecture
                    {
                        Name = architectureFromJSON
                    };
                    context.Add(architecture);
                    context.SaveChanges();
                }

                var systemos = context.SystemOSs.Where(m => m.OperatingSystemId == osname.Id).FirstOrDefault();
                if (systemos == null)
                {
                    systemos = new SystemOS()
                    {
                        SystemId = system.Id,
                        OperatingSystemId = osname.Id,
                        ArchitectureId = architecture.Id,
                        Version = versionFromJSON,
                        LastUpdated = DateTime.Now
                    };
                    context.Add(systemos);
                    context.SaveChanges();
                }

            }
        }

    }
}
