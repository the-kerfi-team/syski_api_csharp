using csharp.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using csharp.Services.WebSockets.Action.Tasks;

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
            bool validPassword = userManager.CheckPasswordAsync(userManager.Users.SingleOrDefault(r => r.Email == _Action.properties.Single(s => s.Key == "email").Value), _Action.properties.Single(s => s.Key == "password").Value).Result;
            if (validPassword)
            {
                var websocketManager = _ServiceProvider.GetService<WebSocketManager>();
                var context = _ServiceProvider.GetService<ApplicationDbContext>();
                var system = new Data.System();
                context.Add(system);
                context.SaveChanges();
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
