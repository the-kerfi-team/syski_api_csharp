using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using csharp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace csharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GPUsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GPUsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("{systemId}")]
        public async Task<IActionResult> GetGPUs(Guid systemId)
        {
            var applicationUserSystem = _context.ApplicationUserSystems
                .Where(u => u.User.Email == ((ClaimsIdentity)User.Identity).FindFirst("email").Value && u.SystemId == systemId).FirstOrDefault();

            if (applicationUserSystem == null)
                return NotFound();

            var GPUs = _context.SystemGPUs.Where(sc => sc.SystemId == systemId).ToList();

            var GPUDTOs = new List<GPUDTO>();

            foreach (var item in GPUs)
            {
                GPUDTOs.Add(CreateDTO(item));
            }

            return Ok(GPUDTOs);
        }

        private GPUDTO CreateDTO(SystemGPU systemGPU)
        {
            var processorModel = _context.ProcessorModels.Find(systemGPU.GPUModelId);
            var architecture = _context.Architectures.Find(processorModel.ArchitectureId);
            var model = _context.Models.Find(processorModel.Id);
            var manufacturer = _context.Manufacturers.Find(model.ManufacturerId);
            var MemoryModel = _context.MemoryModels.Find(systemGPU.GPUModelId);
            var MemoryType = _context.MemoryTypes.Find(MemoryModel.MemoryTypeId);

            var GPUDTO = new GPUDTO()
            {
                Id = systemGPU.GPUModelId,
                ModelName = model.Name,
                ManufacturerName = manufacturer.Name,
                ArchitectureName = architecture.Name,
                ClockSpeed = processorModel.ClockSpeed,
                CoreCount = processorModel.CoreCount,
                ThreadCount = processorModel.ThreadCount,
                MemoryTypeName = MemoryType.Name,
                MemoryBytes = MemoryModel.MemoryBytes
            };

            return GPUDTO;
        }
    }
}