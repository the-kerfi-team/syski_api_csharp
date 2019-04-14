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
    [ApiController]
    public class CPUsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CPUsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        [HttpGet("/system/{systemId}/cpu")]
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

        [Authorize]
        [HttpPost("/system/{systemId}/cpu/data")]
        public async Task<IActionResult> GetCPUData(Guid systemId)
        {
            var applicationUserSystem = _context.ApplicationUserSystems
                .Where(u => u.User.Email == ((ClaimsIdentity)User.Identity).FindFirst("email").Value && u.SystemId == systemId).FirstOrDefault();

            if (applicationUserSystem == null)
                return NotFound();

            var CPUsData = _context.SystemCPUsData.Where(sc => sc.SystemId == systemId).OrderByDescending(i => i.CollectionDateTime).Take(1); ;

            return Ok(CPUsData);
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