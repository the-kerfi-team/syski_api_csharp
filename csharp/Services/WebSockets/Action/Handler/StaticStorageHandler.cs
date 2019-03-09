using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using csharp.Data;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;

namespace csharp.Services.WebSockets.Action.Handler
{
    public class StaticStorageHandler : ActionHandler
    {

        public StaticStorageHandler(IServiceProvider serviceProvider, WebSocket webSocket, Action action) : base(serviceProvider, webSocket, action)
        {
        }

        public override void HandleAction()
        {
            var context = _ServiceProvider.GetService<ApplicationDbContext>();
            var systemUUID = _ServiceProvider.GetService<WebSocketManager>().GetId(_WebSocket);

            var system = context.Systems.Where(u => u.Id == systemUUID).FirstOrDefault();
            if (system != null)
            {
                DateTime lastUpdated = DateTime.Now;
                int slot = 0;
                JArray storageArray = (JArray)_Action.properties.SelectToken("storage");

                foreach (JToken storage in storageArray)
                {
                    string modelFromJSON = (string) storage.SelectToken("model");
                    string manufacturerFromJSON = (string) storage.SelectToken("manufacturer");
                    string interfaceFromJSON = (string) storage.SelectToken("interface");
                    long sizeFromJSON = (long) storage.SelectToken("size");

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

                    var storagemodel = context.StorageModels.Where(t => t.ModelId == model.Id && t.Size == sizeFromJSON).FirstOrDefault();
                    if (storagemodel == null)
                    {
                        storagemodel = new StorageModel
                        {
                            ModelId = model.Id,
                            Size = sizeFromJSON
                        };
                        context.Add(storagemodel);
                        context.SaveChanges();
                    }

                    var interfacetype = context.InterfaceTypes.Where(t => t.Name == interfaceFromJSON).FirstOrDefault();
                    if (interfacetype == null)
                    {
                        interfacetype = new InterfaceType()
                        {
                            Name = interfaceFromJSON
                        };
                        context.Add(interfacetype);
                        context.SaveChanges();
                    }

                    var systemstorage = context.SystemStorages.Where(t => t.SystemId == system.Id && t.StorageModelId == storagemodel.Id && t.Slot == slot).FirstOrDefault();
                    if (systemstorage == null)
                    {
                        systemstorage = new SystemStorage
                        {
                            SystemId = system.Id,
                            Slot = slot,
                            StorageModelId = storagemodel.Id,
                            InterfaceId = interfacetype.Id,
                            LastUpdated = lastUpdated
                        };
                        context.Add(systemstorage);
                        context.SaveChanges();
                    }
                    slot++;
                }
            }
        }

    }
}
