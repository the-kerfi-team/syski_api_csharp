using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using Syski.Data;

namespace Syski.WebSocket.Services.WebSockets.Actions.Handlers
{
    public class UserAuthenticationHandler : AuthenticationHandler
    {

        public UserAuthenticationHandler(Action action, WebSocketConnection webSocketConnection, IServiceProvider serviceProvider) : base(action, webSocketConnection, serviceProvider)
        {
        }

        public override async void HandleAction()
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                ApplicationUser user = userManager.Users.SingleOrDefault(r => r.Email.Equals((string) action.properties.SelectToken("email")));
                if (user != null)
                {
                    bool validPassword = await userManager.CheckPasswordAsync(user, (string) action.properties.SelectToken("password"));
                    if (validPassword)
                    {
                        var context = serviceScope.ServiceProvider.GetService<SyskiDBContext>();
                        var system = new Data.System
                        {
                            LastUpdated = DateTime.Now,
                            Secret = Guid.NewGuid().ToString().Replace("-", "")
                        };
                        context.Add(system);
                        context.SaveChanges();

                        var applicationUserSystems = new ApplicationUserSystems()
                        {
                            UserId = user.Id,
                            SystemId = system.Id
                        };
                        context.Add(applicationUserSystems);
                        context.SaveChanges();

                        await webSocketConnection.SendAction("authentication", new JObject { { "system", system.Id }, { "secret", system.Secret } });
                    }
                }
            }
        }

    }
}
