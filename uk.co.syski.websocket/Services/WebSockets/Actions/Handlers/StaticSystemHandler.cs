using Microsoft.Extensions.DependencyInjection;
using Syski.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Syski.WebSocket.Services.WebSockets.Actions.Handlers
{
    public class StaticSystemHandler : ActionHandler
    {

        public StaticSystemHandler(Action action, WebSocketConnection webSocketConnection, IServiceProvider serviceProvider) : base(action, webSocketConnection, serviceProvider)
        {
        }

        public override void HandleAction()
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<SyskiDBContext>();
                var systemUUID = serviceProvider.GetService<WebSocketManager>().GetSystemId(webSocketConnection.Id);

                var system = context.Systems.Where(u => u.Id == systemUUID).FirstOrDefault();
                if (system != null)
                {
                    string modelFromJSON = (string) action.properties.SelectToken("model");
                    string manufacturerFromJSON = (string) action.properties.SelectToken("manufacturer");
                    string typeFromJSON = (string) action.properties.SelectToken("type");
                    string hostNameFromJSON = (string) action.properties.SelectToken("hostname");

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

                    List<SystemTypeName> systemTypeNameList = new List<SystemTypeName>();
                    SystemTypeName systemTypeName = context.SystemTypeNames.FirstOrDefault(stn => stn.Name.Equals(typeFromJSON));
                    if (systemTypeName == null && typeFromJSON != null)
                    {
                        systemTypeName = new SystemTypeName
                        {
                            Name = typeFromJSON
                        };
                        context.Add(systemTypeName);
                        context.SaveChanges();
                        systemTypeNameList.Add(systemTypeName);
                    }
                    else
                    {
                        systemTypeNameList.Add(systemTypeName);
                    }

                    bool changed = false;
                    List<SystemType> currentSystemTypes = context.SystemTypes.Where(st => st.SystemId.Equals(systemUUID)).ToList();
                    List<SystemType> removeSystemTypes = currentSystemTypes.ToList();
                    foreach (SystemTypeName stn in systemTypeNameList)
                    {
                        bool found = false;
                        foreach(SystemType st in currentSystemTypes)
                        {
                            if (st.TypeId.Equals(stn.Id))
                            {
                                found = true;
                                removeSystemTypes.Remove(st);
                            }
                        }

                        if (!found)
                        {
                            SystemType systemType = new SystemType
                            {
                                SystemId = systemUUID,
                                TypeId = stn.Id
                            };
                            context.Add(systemType);
                            changed = true;
                        }
                    }
                    if (changed)
                    {
                        context.SaveChanges();
                    }

                    if (removeSystemTypes.Count > 0)
                    {
                        foreach (SystemType st in removeSystemTypes)
                        {
                            context.Remove(st);
                        }
                        context.SaveChanges();
                    }

                    system.ModelId = (model != null ? model.Id : (Guid?) null);
                    system.HostName = hostNameFromJSON;
                    system.LastUpdated = DateTime.Now;
                    context.Update(system);
                    context.SaveChanges();
                }
            }
        }

    }
}
