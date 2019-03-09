using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using csharp.Data;
using Microsoft.Extensions.DependencyInjection;

namespace csharp.Services.WebSockets.Action.Handler
{
    public class StaticMotherboardHandler : ActionHandler
    {

        public StaticMotherboardHandler(IServiceProvider serviceProvider, WebSocket webSocket, Action action) : base(serviceProvider, webSocket, action)
        {
        }

        public override void HandleAction()
        {
            var context = _ServiceProvider.GetService<ApplicationDbContext>();
            var systemUUID = _ServiceProvider.GetService<WebSocketManager>().GetId(_WebSocket);

            var system = context.Systems.Where(u => u.Id == systemUUID).FirstOrDefault();
            if (system != null)
            {
                string modelFromJSON = (string) _Action.properties.SelectToken("model");
                string manufacturerFromJSON = (string) _Action.properties.SelectToken("manufacturer");
                string versionFromJSON = (string) _Action.properties.SelectToken("version");

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

                var motherboardmodel = context.MotherboardModels.Where(m => m.ModelId == model.Id).FirstOrDefault();
                if (motherboardmodel == null)
                {
                    motherboardmodel = new MotherboardModel
                    {
                        ModelId = model.Id,
                        Version = versionFromJSON
                    };
                    context.Add(motherboardmodel);
                    context.SaveChanges();
                }

                var systemmotherboard = context.SystemMotherboards.Where(m => m.SystemId == system.Id && m.MotherboardModelId == motherboardmodel.Id).FirstOrDefault();
                if (systemmotherboard == null)
                {
                    systemmotherboard = new SystemMotherboard()
                    {
                        SystemId = system.Id,
                        MotherboardModelId = motherboardmodel.Id,
                        LastUpdated = DateTime.Now
                    };
                    context.Add(systemmotherboard);
                    context.SaveChanges();
                }

            }
        }

    }
}
