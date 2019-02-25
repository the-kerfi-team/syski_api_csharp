using csharp.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace csharp.Services.WebSockets.Action.Handler
{
    public class UserAuthenticationHandler : ActionHandler
    {

        public UserAuthenticationHandler(WebSocket webSocket, Guid? systemId, Action action) : base(webSocket, systemId, action)
        {
            
        }

        public override bool HandleAction()
        {
            //return _userManager.CheckPasswordAsync(_userManager.Users.SingleOrDefault(r => r.Email == _Action.properties.Single(s => s.Key == "email").Value), _Action.properties.Single(s => s.Key == "password").Value).Result;
        }

    }
}
