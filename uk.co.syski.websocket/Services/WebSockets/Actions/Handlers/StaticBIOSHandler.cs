using Microsoft.Extensions.DependencyInjection;
using Syski.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syski.WebSocket.Services.WebSockets.Actions.Handlers
{
    public class StaticBIOSHandler : ActionHandler
    {

        public StaticBIOSHandler(Action action, WebSocketConnection webSocketConnection, IServiceProvider serviceProvider) : base(action, webSocketConnection, serviceProvider)
        {
        }

        public override void HandleAction()
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<SyskiDBContext>();
                var systemUUID = serviceProvider.GetService<WebSocketManager>().GetSystemId(webSocketConnection.Id);

                string manufacturerFromJSON = (string) action.properties.SelectToken("manufacturer");
                string captionFromJSON = (string)action.properties.SelectToken("caption");
                string versionFromJSON = (string) action.properties.SelectToken("version");
                string dateFromJSON = (string) action.properties.SelectToken("date");

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
                var biosModel = context.BIOSModels.FirstOrDefault(m => m.ManufacturerId.Equals(manufacturer));
                if (biosModel == null)
                {
                    biosModel = new BIOSModel
                    {
                        ManufacturerId = manufacturerId
                    };
                    context.Add(biosModel);
                    context.SaveChanges();
                }

                var systemBIOS = context.SystemBIOSs.FirstOrDefault(m => m.SystemId.Equals(systemUUID));
                if (systemBIOS == null)
                {
                    systemBIOS = new SystemBIOS
                    {
                        SystemId = systemUUID,
                        BIOSModelId = biosModel.Id,
                        Caption = captionFromJSON,
                        Version = versionFromJSON,
                        Date = dateFromJSON,
                        LastUpdated = DateTime.Now
                    };
                    context.Add(systemBIOS);
                    context.SaveChanges();
                }
                else
                {
                    if (!systemBIOS.BIOSModelId.Equals(biosModel.Id))
                    {
                        context.Remove(systemBIOS);
                        systemBIOS = new SystemBIOS
                        {
                            SystemId = systemUUID,
                            BIOSModelId = biosModel.Id,
                            Caption = captionFromJSON,
                            Version = versionFromJSON,
                            Date = dateFromJSON,
                            LastUpdated = DateTime.Now
                        };
                        context.Add(systemBIOS);
                        context.SaveChanges();
                    }
                    else
                    {
                        systemBIOS.Caption = captionFromJSON;
                        systemBIOS.Version = versionFromJSON;
                        systemBIOS.Date = dateFromJSON;
                        systemBIOS.LastUpdated = DateTime.Now;
                        context.Update(systemBIOS);
                        context.SaveChanges();
                    }
                }

            }
        }

    }
}
