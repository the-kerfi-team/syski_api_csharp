using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using csharp.Data;
using Microsoft.Extensions.DependencyInjection;

namespace csharp.Services.WebSockets.Action.Handler
{
    public class StaticCPUHandler : ActionHandler
    {

        public StaticCPUHandler(IServiceProvider serviceProvider, WebSocket webSocket, Action action) : base(serviceProvider, webSocket, action)
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
                string architectureFromJSON = (string)_Action.properties.SelectToken("architecture");
                int clockspeedFromJSON = (int)_Action.properties.SelectToken("clockspeed");
                int corecountFromJSON = (int)_Action.properties.SelectToken("corecount");
                int threadcountFromJSON = (int)_Action.properties.SelectToken("threadcount");

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

                var architecture = context.Architectures.Where(m => m.Name == architectureFromJSON).FirstOrDefault();
                if (architecture == null)
                {
                    architecture = new Architecture
                    {
                        Name = architectureFromJSON
                    };
                    context.Add(architecture);
                    context.SaveChanges();
                }

                var processormodel = context.CPUModels.Where(m => m.ModelId == model.Id && m.ArchitectureId == architecture.Id).FirstOrDefault();
                if (processormodel == null)
                {
                    processormodel = new CPUModel
                    {
                        ModelId = model.Id,
                        ArchitectureId = architecture.Id
                    };
                    context.Add(processormodel);
                    context.SaveChanges();
                }

                var systemcpu = context.SystemCPUs.Where(m => m.SystemId == system.Id).FirstOrDefault();
                if (systemcpu == null)
                {
                    systemcpu = new SystemCPU
                    {
                        SystemId = system.Id,
                        CPUModelID = processormodel.Id,
                        ClockSpeed = clockspeedFromJSON,
                        CoreCount = corecountFromJSON,
                        ThreadCount = threadcountFromJSON
                    };
                    context.Add(systemcpu);
                    context.SaveChanges();
                }
            }
        }

    }
}
