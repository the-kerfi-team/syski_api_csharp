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
    public class GPUsController : ControllerBase
    {

        private readonly SyskiDBContext context;

        public GPUsController(SyskiDBContext context)
        {
            this.context = context;
        }

        [Authorize]
        [HttpGet("/system/{systemId}/gpu")]
        public IActionResult GetGPUs(Guid systemId)
        {
            var applicationUserSystem = context.ApplicationUserSystems.FirstOrDefault(u => u.User.Email.Equals(((ClaimsIdentity) User.Identity).FindFirst("email").Value) && u.SystemId.Equals(systemId));

            if (applicationUserSystem == null)
                return NotFound();

            var lastUpdatedGPU = context.SystemGPUs.Where(sc => sc.SystemId.Equals(systemId)).OrderByDescending(i => i.LastUpdated).FirstOrDefault();
            DateTime? lastUpdated = (lastUpdatedGPU != null ? lastUpdatedGPU.LastUpdated : (DateTime?)null);
            var GPUs = context.SystemGPUs.Where(sc => sc.SystemId.Equals(systemId) && sc.LastUpdated.Equals(lastUpdated)).ToList();

            var GPUDTOs = new List<GPUDTO>();

            foreach (var item in GPUs)
            {
                GPUDTOs.Add(CreateGPUDTO(item));
            }

            return Ok(GPUDTOs);
        }

        private GPUDTO CreateGPUDTO(SystemGPU systemGPU)
        {
            var gpuModel = context.GPUModels.Find(systemGPU.GPUModelId);

            var gpuDTO = new GPUDTO()
            {
                Id = systemGPU.GPUModelId
            };

            if (gpuModel.ModelId != null)
            {
                var model = context.Models.Find(gpuModel.ModelId);
                gpuDTO.ModelName = model.Name;
                if (model.ManufacturerId != null)
                {
                    var manufacturer = context.Manufacturers.Find(model.ManufacturerId);
                    gpuDTO.ManufacturerName = manufacturer.Name;
                }
            }

            return gpuDTO;
        }

    }
}
