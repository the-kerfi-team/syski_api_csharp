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

        public CommandsController(ApplicationDbContext context, WebSocketManager WebSocketManager)
        {
            _context = context;
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

            WebSocket websocket = _socketManager.GetSocketById(systemId);
            if (websocket == null)
            {
                return NotFound();
            }

            string shutdownAction = JsonConvert.SerializeObject(ActionFactory.createAction("shutdown"));
            websocket.SendAsync(buffer: new ArraySegment<byte>(array: Encoding.ASCII.GetBytes(shutdownAction), offset: 0, count: shutdownAction.Length), messageType: WebSocketMessageType.Text, endOfMessage: true, cancellationToken: CancellationToken.None);

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

            WebSocket websocket = _socketManager.GetSocketById(systemId);
            if (websocket == null)
            {
                return NotFound();
            }

            string shutdownAction = JsonConvert.SerializeObject(ActionFactory.createAction("restart"));
            websocket.SendAsync(buffer: new ArraySegment<byte>(array: Encoding.ASCII.GetBytes(shutdownAction), offset: 0, count: shutdownAction.Length), messageType: WebSocketMessageType.Text, endOfMessage: true, cancellationToken: CancellationToken.None);

            return Ok();
        }

    }
}
