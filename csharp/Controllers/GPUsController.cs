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
    public class GPUsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GPUsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("/system/{systemId}/gpu")]
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
            var processorModel = _context.GPUModels.Find(systemGPU.GPUModelId);
            var model = _context.Models.Find(processorModel.ModelId);
            var manufacturer = _context.Manufacturers.Find(model.ManufacturerId);

            var GPUDTO = new GPUDTO()
            {
                Id = systemGPU.GPUModelId,
                ModelName = model.Name,
                ManufacturerName = manufacturer.Name,
            };

            return GPUDTO;
        }
    }
}