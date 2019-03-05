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
    public class StaticRAMHandler : ActionHandler
    {

        public StaticRAMHandler(IServiceProvider serviceProvider, WebSocket webSocket, Action action) : base(serviceProvider, webSocket, action)
        {
        }

        public override void HandleAction()
        {
            var context = _ServiceProvider.GetService<ApplicationDbContext>();
            var systemUUID = _ServiceProvider.GetService<WebSocketManager>().GetId(_WebSocket);

            var system = context.Systems.Where(u => u.Id == systemUUID).FirstOrDefault();
            if (system != null)
            {
                JArray ramArray = (JArray)_Action.properties.SelectToken("ram");
                foreach (JToken ram in ramArray)
                {
                    string modelFromJSON = (string) ram.SelectToken("model");
                    string manufacturerFromJSON = (string) ram.SelectToken("manufacturer");
                    string typeFromJSON = (string) ram.SelectToken("type");
                    int speedFromJSON = (int) ram.SelectToken("speed");
                    long sizeFromJSON = (long) ram.SelectToken("size");

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

                    var storagetype = context.StorageTypes.Where(t => t.Name == typeFromJSON).FirstOrDefault();
                    if (storagetype == null)
                    {
                        storagetype = new StorageType
                        {
                            Name = typeFromJSON
                        };
                        context.Add(storagetype);
                        context.SaveChanges();
                    }

                    var rammodel = context.RAMModels.Where(t => t.ModelId == model.Id && t.Size == sizeFromJSON).FirstOrDefault();
                    if (rammodel == null)
                    {
                        rammodel = new RAMModel
                        {
                            ModelId = model.Id,
                            Size = sizeFromJSON
                        };
                        context.Add(rammodel);
                        context.SaveChanges();
                    }

                    var systemram = context.SystemRAMs.Where(t => t.SystemId == system.Id && t.RAMModelId == rammodel.Id).FirstOrDefault();
                    if (systemram == null)
                    {
                        systemram = new SystemRAM
                        {
                            SystemId = system.Id,
                            RAMModelId = rammodel.Id,
                            Speed = speedFromJSON,
                            TypeId = storagetype.Id,
                            LastUpdated = DateTime.Now
                        };
                        context.Add(systemram);
                        context.SaveChanges();
                    }

                }
            }
        }

    }
}
