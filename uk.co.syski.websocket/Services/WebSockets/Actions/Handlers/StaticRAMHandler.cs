using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Syski.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syski.WebSocket.Services.WebSockets.Actions.Handlers
{
    public class StaticRAMHandler : ActionHandler
    {
        public StaticRAMHandler(Action action, WebSocketConnection webSocketConnection, IServiceProvider serviceProvider) : base(action, webSocketConnection, serviceProvider)
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

                JArray ramArray = (JArray) action.properties.SelectToken("ram");
                foreach (JToken ram in ramArray)
                {
                    string modelFromJSON = (string) ram.SelectToken("model");
                    string manufacturerFromJSON = (string) ram.SelectToken("manufacturer");
                    string typeFromJSON = (string) ram.SelectToken("type");
                    int speedFromJSON = (int) ram.SelectToken("speed");
                    long sizeFromJSON = (long) ram.SelectToken("size");

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
                    var ramModel = context.RAMModels.FirstOrDefault(m => m.ModelId.Equals(modelId) && m.Size.Equals(sizeFromJSON));
                    if (ramModel == null)
                    {
                        ramModel = new RAMModel
                        {
                            ModelId = modelId,
                            Size = sizeFromJSON
                        };
                        context.Add(ramModel);
                        context.SaveChanges();
                    }

                    var systemRAM = context.SystemRAMs.Where(m => m.SystemId.Equals(systemUUID) && m.Slot.Equals(slot)).FirstOrDefault();
                    if (systemRAM == null)
                    {
                        systemRAM = new SystemRAM
                        {
                            SystemId = systemUUID,
                            RAMModelId = ramModel.Id,
                            Slot = slot,
                            Speed = speedFromJSON,
                            LastUpdated = lastUpdated
                        };
                        context.Add(systemRAM);
                        context.SaveChanges();
                    }
                    else
                    {
                        if (!systemRAM.RAMModelId.Equals(ramModel.Id))
                        {
                            context.Remove(systemRAM);
                            systemRAM = new SystemRAM
                            {
                                SystemId = systemUUID,
                                RAMModelId = ramModel.Id,
                                Slot = slot,
                                Speed = speedFromJSON,
                                LastUpdated = lastUpdated
                            };
                            context.Add(systemRAM);
                            context.SaveChanges();
                        }
                        else
                        {
                            systemRAM.Speed = speedFromJSON;
                            systemRAM.LastUpdated = lastUpdated;
                            context.Update(systemRAM);
                            context.SaveChanges();
                        }
                    }
                    slot++;
                }
            }

        }

    }
}
