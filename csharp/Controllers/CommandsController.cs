using csharp.Data;
using csharp.Services.WebSockets;
using csharp.Services.WebSockets.Action;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace csharp.Controllers
{
    [ApiController]
    public class CommandsController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly WebSocketManager _socketManager;

        public CommandsController(WebSocketManager WebSocketManager)
        {
            _context = new ApplicationDbContext();
            _socketManager = WebSocketManager;
        }

        [Authorize]
        [HttpPost("/system/{systemId}/shutdown")]
        public IActionResult CmdShutdown(Guid systemId)
        {
            var applicationUserSystem = _context.ApplicationUserSystems
                .Where(u => u.User.Email == ((ClaimsIdentity) User.Identity).FindFirst("email").Value && u.SystemId == systemId).FirstOrDefault();

            if (applicationUserSystem == null)
                return NotFound();

            WebSocketConnection websocket = _socketManager.GetSocketById(systemId);
            if (websocket == null)
            {
                return NotFound();
            }
            websocket.sendAction("shutdown");

            return Ok();
        }

        [Authorize]
        [HttpPost("/system/{systemId}/restart")]
        public IActionResult CmdRestart(Guid systemId)
        {
            var applicationUserSystem = _context.ApplicationUserSystems
                .Where(u => u.User.Email == ((ClaimsIdentity)User.Identity).FindFirst("email").Value && u.SystemId == systemId).FirstOrDefault();

            if (applicationUserSystem == null)
                return NotFound();

            WebSocketConnection websocket = _socketManager.GetSocketById(systemId);
            if (websocket == null)
            {
                return NotFound();
            }
            websocket.sendAction("restart");

            return Ok();
        }

    }
}
