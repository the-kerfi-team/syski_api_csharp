using csharp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace csharp.Services.WebSockets.Action.Handler
{
    public class StaticSystemHandler : ActionHandler
    {

        public StaticSystemHandler(IServiceProvider serviceProvider, WebSocket webSocket, Action action) : base(serviceProvider, webSocket, action)
        {

        }

        public override void HandleAction()
        {
            var context = _ServiceProvider.GetService<ApplicationDbContext>();
            var systemUUID = _ServiceProvider.GetService<WebSocketManager>().GetId(_WebSocket);

            var system = context.Systems.Where(u => u.Id == systemUUID).FirstOrDefault();
            if (system != null)
            {
                string modelFromJSON = _Action.properties.FirstOrDefault(x => x.Key == "model").Value;
                string manufacturerFromJSON = _Action.properties.FirstOrDefault(x => x.Key == "manufacturer").Value;
                string typeFromJSON = _Action.properties.FirstOrDefault(x => x.Key == "type").Value;
                string hostNameFromJSON = _Action.properties.FirstOrDefault(x => x.Key == "hostname").Value;

                var manufacturer = context.Manufacturers.Where(m => m.Name == manufacturerFromJSON).FirstOrDefault();
                if (manufacturer == null)
                {
                    manufacturer = new Manufacturer
                    {
                        Name = manufacturerFromJSON
                    };
                    context.Add(manufacturer);
                    context.SaveChanges();
                }

                var model = context.Models.Where(m => m.Name == modelFromJSON && m.ManufacturerId == manufacturer.Id).FirstOrDefault();
                if (model == null)
                {
                    model = new Model
                    {
                        ManufacturerId = manufacturer.Id,
                        Name = modelFromJSON
                    };
                    context.Add(model);
                    context.SaveChanges();
                }

                var types = context.Types.Where(t => t.Name == typeFromJSON).FirstOrDefault();
                if (types == null)
                {
                    types = new Data.Type
                    {
                        Name = typeFromJSON
                    };
                    context.Add(types);
                    context.SaveChanges();
                }

                var modelTypes = context.SystemModelTypes.Where(sm => sm.SystemId == system.Id && sm.TypeId == types.Id);
                if (modelTypes == null)
                {
                    var systemModelTypes = new SystemModelType
                    {
                        SystemId = systemUUID,
                        TypeId = types.Id
                    };
                    context.Add(systemModelTypes);
                    context.SaveChanges();
                }
                system.Model = model;
                system.HostName = hostNameFromJSON;

                context.Update(system);
                context.SaveChanges();
            }
            else
            {
                throw new NullReferenceException();
            }
        }
    }
}
