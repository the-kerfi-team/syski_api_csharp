using Microsoft.Extensions.DependencyInjection;
using Syski.Data;
using Syski.WebSocket.Services.WebSockets.Actions.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syski.WebSocket.Services.WebSockets.Actions.Handlers
{
    public class StaticGPUHandler : ActionHandler
    {

        public StaticGPUHandler(Action action, WebSocketConnection webSocketConnection, IServiceProvider serviceProvider) : base(action, webSocketConnection, serviceProvider)
        {
        }

        public override void HandleAction()
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<SyskiDBContext>();
                var systemUUID = serviceProvider.GetService<WebSocketManager>().GetSystemId(webSocketConnection.Id);
                DateTime lastUpdated = DateTime.Now;
                int slot = 0;

                string modelFromJSON = (string) action.properties.SelectToken("model");
                string manufacturerFromJSON = (string) action.properties.SelectToken("manufacturer");

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

                Guid? manufacturerId = (manufacturer != null ? manufacturer.Id : (Guid?)null);
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

                Guid? modelId = (model != null ? model.Id : (Guid?)null);
                var gpuModel = context.GPUModels.FirstOrDefault(m => m.ModelId.Equals(modelId));
                if (gpuModel == null)
                {
                    gpuModel = new GPUModel
                    {
                        ModelId = modelId,
                    };
                    context.Add(gpuModel);
                    context.SaveChanges();
                }

                var systemGPU = context.SystemGPUs.Where(m => m.SystemId.Equals(systemUUID) && m.Slot.Equals(slot)).FirstOrDefault();
                if (systemGPU == null)
                {
                    systemGPU = new SystemGPU
                    {
                        SystemId = systemUUID,
                        GPUModelId = gpuModel.Id,
                        Slot = slot,
                        LastUpdated = lastUpdated
                    };
                    context.Add(systemGPU);
                    context.SaveChanges();
                }
                else
                {
                    if (!systemGPU.GPUModelId.Equals(gpuModel.Id))
                    {
                        context.Remove(systemGPU);
                        systemGPU = new SystemGPU
                        {
                            SystemId = systemUUID,
                            GPUModelId = gpuModel.Id,
                            Slot = slot,
                            LastUpdated = lastUpdated
                        };
                        context.Add(systemGPU);
                        context.SaveChanges();
                    }
                    else
                    {
                        systemGPU.LastUpdated = lastUpdated;
                        context.Update(systemGPU);
                        context.SaveChanges();
                    }
                }

                //slot++;
            }
        }

    }
}
