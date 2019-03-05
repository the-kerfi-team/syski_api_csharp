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
    public class CPUsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CPUsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("{systemId}")]
        public async Task<IActionResult> GetCPUs (Guid systemId)
        {
            var applicationUserSystem = _context.ApplicationUserSystems
                .Where(u => u.User.Email == ((ClaimsIdentity)User.Identity).FindFirst("email").Value && u.SystemId == systemId).FirstOrDefault();

            if (applicationUserSystem == null)
                return NotFound();

            var CPUs = _context.SystemCPUs.Where(sc => sc.SystemId == systemId).ToList();

            var CPUDTOs = new List<CPUDTO>();

            foreach (var item in CPUs)
            {
                CPUDTOs.Add(CreateDTO(item));
            }

            return Ok(CPUDTOs);
        }

        private CPUDTO CreateDTO(SystemCPU systemCPU)
        {
            var processorModel = _context.CPUModels.Find(systemCPU.CPUModelID);
            var architecture = _context.Architectures.Find(processorModel.ArchitectureId);
            var model = _context.Models.Find(processorModel.ModelId);
            var manufacturer = _context.Manufacturers.Find(model.ManufacturerId);

            var CPUDTO = new CPUDTO()
            {
                Id = systemCPU.CPUModelID,
                ModelName = model.Name,
                ManufacturerName = manufacturer.Name,
                ArchitectureName = architecture.Name,
                ClockSpeed = systemCPU.ClockSpeed,
                CoreCount = systemCPU.CoreCount,
                ThreadCount = systemCPU.ThreadCount
            };

            return CPUDTO;
        }
    }
}