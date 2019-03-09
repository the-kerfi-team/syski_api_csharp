using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using csharp.Data;
using Microsoft.Extensions.DependencyInjection;

namespace csharp.Services.WebSockets.Action.Handler
{
    public class StaticGPUHandler : ActionHandler
    {

        public StaticGPUHandler(IServiceProvider serviceProvider, WebSocket webSocket, Action action) : base(serviceProvider, webSocket, action)
        {
        }

        public override void HandleAction()
        {
            var context = _ServiceProvider.GetService<ApplicationDbContext>();
            var systemUUID = _ServiceProvider.GetService<WebSocketManager>().GetId(_WebSocket);

            var system = context.Systems.Where(u => u.Id == systemUUID).FirstOrDefault();
            if (system != null)
            {
                string modelFromJSON = (string)_Action.properties.SelectToken("model");
                string manufacturerFromJSON = (string)_Action.properties.SelectToken("manufacturer");

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

                var gpumodel = context.GPUModels.Where(m => m.ModelId == model.Id).FirstOrDefault();
                if (gpumodel == null)
                {
                    gpumodel = new GPUModel
                    {
                        ModelId = model.Id
                    };
                    context.Add(gpumodel);
                    context.SaveChanges();
                }

                var systemgpu = context.SystemGPUs.Where(m => m.SystemId == system.Id && m.GPUModelId == gpumodel.Id).FirstOrDefault();
                if (systemgpu == null)
                {
                    systemgpu = new SystemGPU()
                    {
                        SystemId = system.Id,
                        GPUModelId = gpumodel.Id,
                        LastUpdated = DateTime.Now
                    };
                    context.Add(systemgpu);
                    context.SaveChanges();
                }

            }
        }

    }
}
