using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using csharp.Data;
using csharp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace csharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OSsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OSsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("{systemId}")]
        public async Task<IActionResult> GetOSs(Guid systemId)
        {
            var applicationUserSystem = _context.ApplicationUserSystems
                .Where(u => u.User.Email == ((ClaimsIdentity)User.Identity).FindFirst("email").Value && u.SystemId == systemId).FirstOrDefault();

            if (applicationUserSystem == null)
                return NotFound();

            var SystemOSs = _context.SystemOSs.Where(so => so.SystemId == systemId).ToList();

            var OSDTOs = new List<OSDTO>();
            foreach(var item in SystemOSs)
            {
                OSDTOs.Add(CreateDTO(item));
            }

            return Ok(OSDTOs);
        }

        private OSDTO CreateDTO(SystemOS SystemOS)
        {
            var OperatingSystem = _context.OperatingSystems.Find(SystemOS.OSId);
            var Architecture = _context.Architectures.Find(SystemOS.ArchitectureId);

            var OSDTO = new OSDTO()
            {
                Id = SystemOS.OSId,
                Name = OperatingSystem.Name,
                ArchitectureName = Architecture.Name,
                Version = SystemOS.Version
            };

            return OSDTO;
        }
    }
}