using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Syski.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syski.WebSocket.Services.WebSockets.Actions.Handlers
{
    public class StaticStorageHandler : ActionHandler
    {

        public StaticStorageHandler(Action action, WebSocketConnection webSocketConnection, IServiceProvider serviceProvider) : base(action, webSocketConnection, serviceProvider)
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
                JArray storageArray = (JArray)action.properties.SelectToken("storage");

                foreach (JToken storage in storageArray)
                {
                    string modelFromJSON = (string) storage.SelectToken("model");
                    string manufacturerFromJSON = (string) storage.SelectToken("manufacturer");
                    string interfaceFromJSON = (string) storage.SelectToken("interface");
                    long sizeFromJSON = (long) storage.SelectToken("size");

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
                    var storageModel = context.StorageModels.FirstOrDefault(m => m.ModelId.Equals(modelId) && m.Size.Equals(sizeFromJSON));
                    if (storageModel == null)
                    {
                        storageModel = new StorageModel
                        {
                            ModelId = modelId,
                            Size = sizeFromJSON
                        };
                        context.Add(storageModel);
                        context.SaveChanges();
                    }

                    var interfacetype = context.StorageInterfaceTypes.Where(t => t.Name == interfaceFromJSON).FirstOrDefault();
                    if (interfacetype == null && interfaceFromJSON != null)
                    {
                        interfacetype = new StorageInterfaceType()
                        {
                            Name = interfaceFromJSON
                        };
                        context.Add(interfacetype);
                        context.SaveChanges();
                    }

                    Guid? interfacetypeId = (interfacetype != null ? interfacetype.Id : (Guid?) null);
                    var systemStorage = context.SystemStorages.Where(m => m.SystemId.Equals(systemUUID) && m.Slot.Equals(slot)).FirstOrDefault();
                    if (systemStorage == null)
                    {
                        systemStorage = new SystemStorage
                        {
                            SystemId = systemUUID,
                            StorageModelId = storageModel.Id,
                            Slot = slot,
                            StorageInterfaceId = interfacetypeId,
                            LastUpdated = lastUpdated
                        };
                        context.Add(systemStorage);
                        context.SaveChanges();
                    }
                    else
                    {
                        if (!systemStorage.StorageModelId.Equals(storageModel.Id))
                        {
                            context.Remove(systemStorage);
                            systemStorage = new SystemStorage
                            {
                                SystemId = systemUUID,
                                StorageModelId = storageModel.Id,
                                Slot = slot,
                                StorageInterfaceId = interfacetypeId,
                                LastUpdated = lastUpdated
                            };
                            context.Add(systemStorage);
                            context.SaveChanges();
                        }
                        else
                        {
                            systemStorage.StorageInterfaceId = interfacetypeId;
                            systemStorage.LastUpdated = lastUpdated;
                            context.Update(systemStorage);
                            context.SaveChanges();
                        }
                    }
                    slot++;
                }
            }

        }

    }
}
