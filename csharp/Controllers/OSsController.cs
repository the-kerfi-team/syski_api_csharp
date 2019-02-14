using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using csharp.Data;
using csharp.Models;
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

        [HttpGet("{SystemId}")]
        public async Task<IActionResult> GetOSs(Guid SystemId)
        {
            if (_context.Systems.Find(SystemId) == null)
                return NotFound();

            var SystemOSs = _context.SystemOSs.Where(so => so.SystemId == SystemId).ToList();

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