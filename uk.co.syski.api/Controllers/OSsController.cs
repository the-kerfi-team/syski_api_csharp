using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Syski.API.Models;
using Syski.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Syski.API.Controllers
{
    [ApiController]
    public class OSsController : ControllerBase
    {

        private readonly SyskiDBContext context;

        public OSsController(SyskiDBContext context)
        {
            this.context = context;
        }

        [Authorize]
        [HttpGet("/system/{systemId}/operatingsystem")]
        public IActionResult GetOSs(Guid systemId)
        {
            var applicationUserSystem = context.ApplicationUserSystems.FirstOrDefault(u => u.User.Email.Equals(((ClaimsIdentity) User.Identity).FindFirst("email").Value) && u.SystemId.Equals(systemId));

            if (applicationUserSystem == null)
                return NotFound();

            var SystemOSs = context.SystemOSs.Where(so => so.SystemId == systemId).ToList();

            var OSDTOs = new List<OSDTO>();
            foreach (var item in SystemOSs)
            {
                OSDTOs.Add(CreateOSDTO(item));
            }

            return Ok(OSDTOs);
        }

        private OSDTO CreateOSDTO(SystemOS systemOS)
        {
            var OperatingSystem = context.OperatingSystemModels.Find(systemOS.OperatingSystemId);

            var OSDTO = new OSDTO()
            {
                Id = systemOS.OperatingSystemId,
                Name = OperatingSystem.Name,
                Version = systemOS.Version
            };

            if (systemOS.ArchitectureId != null)
            {
                var architecture = context.Architectures.Find(systemOS.ArchitectureId);
                OSDTO.ArchitectureName = architecture.Name;
            }

            return OSDTO;
        }

    }
}
