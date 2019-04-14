using csharp.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace csharp.Services.WebSockets.Action.Handler
{
    public class UserAuthenticationHandler : ActionHandler
    {

        public UserAuthenticationHandler(IServiceProvider serviceProvider, WebSocketConnection webSocket, Action action) : base(serviceProvider, webSocket, action)
        {
            
        }

        public override async void HandleAction()
        {
            var userManager = _ServiceProvider.GetService<UserManager<ApplicationUser>>();
            ApplicationUser user = userManager.Users.SingleOrDefault(r => r.Email == (string)_Action.properties.SelectToken("email"));
            if (user != null)
            {
                bool validPassword = userManager.CheckPasswordAsync(user, (string)_Action.properties.SelectToken("password")).Result;
                if (validPassword)
                {
                    var websocketManager = _ServiceProvider.GetService<WebSocketManager>();
                    var context = _ServiceProvider.GetService<ApplicationDbContext>();
                    var system = new Data.System
                    {
                        LastUpdated = DateTime.Now, Secret = Guid.NewGuid().ToString().Replace("-", "")
                    };
                    context.Add(system);
                    context.SaveChanges();

                    var applicationUserSystems = new Data.ApplicationUserSystems()
                    {
                        UserId = user.Id,
                        SystemId = system.Id
                    };
                    context.Add(applicationUserSystems);
                    context.SaveChanges();

                    var authJson = new JObject {{"system", system.Id}, {"secret", system.Secret}};

                    await _WebSocket.sendAction("authentication", authJson);

                    websocketManager.AddSocket(_WebSocket.Id, _WebSocket);
                    websocketManager.AddSystemToSocketLink(_WebSocket.Id, system.Id);
                    string[] actions = { "staticsystem", "staticcpu", "staticram", "staticos", "staticgpu", "staticmotherboard", "staticstorage", "staticbios" };
                    foreach (string action in actions)
                    {
                        _WebSocket.sendAction(action);
                        Thread.Sleep(100);
                    }
                }
            }

        }

    }
}
