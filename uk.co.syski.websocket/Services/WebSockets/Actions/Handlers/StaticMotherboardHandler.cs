using Microsoft.Extensions.DependencyInjection;
using Syski.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syski.WebSocket.Services.WebSockets.Actions.Handlers
{
    public class StaticMotherboardHandler : ActionHandler
    {

        public StaticMotherboardHandler(Action action, WebSocketConnection webSocketConnection, IServiceProvider serviceProvider) : base(action, webSocketConnection, serviceProvider)
        {
        }

        public override void HandleAction()
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<SyskiDBContext>();
                var systemUUID = serviceProvider.GetService<WebSocketManager>().GetSystemId(webSocketConnection.Id);

                string modelFromJSON = (string) action.properties.SelectToken("model");
                string manufacturerFromJSON = (string) action.properties.SelectToken("manufacturer");
                string versionFromJSON = (string) action.properties.SelectToken("version");

                Manufacturer manufacturer = context.Manufacturers.Where(m => m.Name == manufacturerFromJSON).FirstOrDefault();
                if (manufacturer == null && manufacturerFromJSON != null)
                {
                    manufacturer = new Manufacturer
                    {
                        Name = manufacturerFromJSON
                    };
                    context.Add(manufacturer);
                    context.SaveChanges();
                }

                Guid? manufacturerId = (manufacturer != null ? manufacturer.Id : (Guid?) null);
                Model model = context.Models.FirstOrDefault(m => m.Name.Equals(modelFromJSON) && m.ManufacturerId.Equals(manufacturerId));
                if ((model == null) && ((modelFromJSON != null && manufacturer == null) || (modelFromJSON != null || manufacturer != null)))
                {
                    model = new Model
                    {
                        Name = modelFromJSON
                    };
                    if (manufacturer != null)
                    {
                        model.ManufacturerId = manufacturer.Id;
                    }
                    context.Add(model);
                    context.SaveChanges();
                }

                Guid? modelId = (model != null ? model.Id : (Guid?) null);
                var motherboardModel = context.MotherboardModels.FirstOrDefault(m => m.ModelId.Equals(modelId) && m.Version.Equals(versionFromJSON));
                if (motherboardModel == null)
                {
                    motherboardModel = new MotherboardModel
                    {
                        ModelId = modelId,
                        Version = versionFromJSON
                    };
                    context.Add(motherboardModel);
                    context.SaveChanges();
                }

                var systemMotherboard = context.SystemMotherboards.FirstOrDefault(m => m.SystemId.Equals(systemUUID));
                if (systemMotherboard == null)
                {
                    systemMotherboard = new SystemMotherboard
                    {
                        SystemId = systemUUID,
                        MotherboardModelId = motherboardModel.Id,
                        LastUpdated = DateTime.Now
                    };
                    context.Add(systemMotherboard);
                    context.SaveChanges();
                }
                else
                {
                    if (!systemMotherboard.MotherboardModelId.Equals(motherboardModel.Id))
                    {
                        context.Remove(systemMotherboard);
                        systemMotherboard = new SystemMotherboard
                        {
                            SystemId = systemUUID,
                            MotherboardModelId = motherboardModel.Id,
                            LastUpdated = DateTime.Now
                        };
                        context.Add(systemMotherboard);
                        context.SaveChanges();
                    }
                    else
                    {
                        systemMotherboard.LastUpdated = DateTime.Now;
                        context.Update(systemMotherboard);
                        context.SaveChanges();
                    }
                }

            }
        }

    }
}
