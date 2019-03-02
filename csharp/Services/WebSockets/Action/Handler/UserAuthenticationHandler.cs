using csharp.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using csharp.Services.WebSockets.Action.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace csharp.Services.WebSockets.Action.Handler
{
    public class UserAuthenticationHandler : ActionHandler
    {

        public UserAuthenticationHandler(IServiceProvider serviceProvider, WebSocket webSocket, Action action) : base(serviceProvider, webSocket, action)
        {
            
        }

        public override void HandleAction()
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
                    var system = new Data.System();
                    system.LastUpdated = DateTime.Now;
                    system.Secret = Guid.NewGuid().ToString().Replace("-", "");
                    context.Add(system);
                    context.SaveChanges();

                    var applicationUserSystems = new Data.ApplicationUserSystems()
                    {
                        UserId = user.Id,
                        SystemId = system.Id
                    };
                    context.Add(applicationUserSystems);
                    context.SaveChanges();

                    var authJson = new JObject();
                    authJson.Add("system", system.Id);
                    authJson.Add("secret", system.Secret);

                    SendMessageAsync(JsonConvert.SerializeObject(ActionFactory.createAction("authentication", authJson)));

                    websocketManager.AddSocket(system.Id, _WebSocket);
                    websocketManager.addTask(new ActionTask()
                    {
                        action = ActionFactory.createAction("staticsystem"),
                        delay = 86400,
                        repeat = true,
                        webSocket = _WebSocket,
                        runAtDateTime = DateTime.Now
                    });
                }
            }

        }

    }
}
