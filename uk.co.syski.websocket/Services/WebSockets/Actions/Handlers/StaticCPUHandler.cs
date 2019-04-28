using Microsoft.Extensions.DependencyInjection;
using Syski.Data;
using System;
using System.Linq;

namespace Syski.WebSocket.Services.WebSockets.Actions.Handlers
{
    public class StaticCPUHandler : ActionHandler
    {

        public StaticCPUHandler(Action action, WebSocketConnection webSocketConnection, IServiceProvider serviceProvider) : base(action, webSocketConnection, serviceProvider)
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
                string architectureFromJSON = (string) action.properties.SelectToken("architecture");
                int clockspeedFromJSON = (int) action.properties.SelectToken("clockspeed");
                int corecountFromJSON = (int) action.properties.SelectToken("corecount");
                int threadcountFromJSON = (int) action.properties.SelectToken("threadcount");

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

                var architecture = context.Architectures.FirstOrDefault(m => m.Name.Equals(architectureFromJSON));
                if (architecture == null && architectureFromJSON != null)
                {
                    architecture = new Architecture
                    {
                        Name = architectureFromJSON
                    };
                    context.Add(architecture);
                    context.SaveChanges();
                }

                Guid? modelId = (model != null ? model.Id : (Guid?) null);
                Guid? architectureId = (architecture != null ? architecture.Id : (Guid?) null);
                var cpuModel = context.CPUModels.FirstOrDefault(m => m.ModelId.Equals(modelId) && m.ArchitectureId.Equals(architectureId));
                if (cpuModel == null)
                {
                    cpuModel = new CPUModel
                    {
                        ModelId = modelId,
                        ArchitectureId = architectureId
                    };
                    context.Add(cpuModel);
                    context.SaveChanges();
                }

                var systemCPU = context.SystemCPUs.Where(m => m.SystemId.Equals(systemUUID) && m.Slot.Equals(slot)).FirstOrDefault();
                if (systemCPU == null)
                {
                    systemCPU = new SystemCPU
                    {
                        SystemId = systemUUID,
                        CPUModelID = cpuModel.Id,
                        ClockSpeed = clockspeedFromJSON,
                        CoreCount = corecountFromJSON,
                        ThreadCount = threadcountFromJSON,
                        LastUpdated = lastUpdated
                    };
                    context.Add(systemCPU);
                    context.SaveChanges();
                }
                else
                {
                    if (!systemCPU.CPUModelID.Equals(cpuModel.Id))
                    {
                        context.Remove(systemCPU);
                        systemCPU = new SystemCPU
                        {
                            SystemId = systemUUID,
                            CPUModelID = cpuModel.Id,
                            ClockSpeed = clockspeedFromJSON,
                            CoreCount = corecountFromJSON,
                            ThreadCount = threadcountFromJSON,
                            LastUpdated = lastUpdated
                        };
                        context.Add(systemCPU);
                        context.SaveChanges();
                    }
                    else
                    {
                        systemCPU.ClockSpeed = clockspeedFromJSON;
                        systemCPU.CoreCount = corecountFromJSON;
                        systemCPU.ThreadCount = threadcountFromJSON;
                        systemCPU.LastUpdated = lastUpdated;
                        context.Update(systemCPU);
                        context.SaveChanges();
                    }
                }

                //slot++;
            }
        }

    }
}
