using csharp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;

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
                //List<JToken> jsonData = _Action.properties["properties"].Children().ToList();
                string modelFromJSON = (string)_Action.properties.SelectToken("model");
                string manufacturerFromJSON = (string)_Action.properties.SelectToken("manufacturer");
                string typeFromJSON = (string)_Action.properties.SelectToken("type");
                string hostNameFromJSON = (string)_Action.properties.SelectToken("hostname");

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
