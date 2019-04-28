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
    public class CPUsController : ControllerBase
    {

        private readonly SyskiDBContext context;

        public CPUsController(SyskiDBContext context)
        {
            this.context = context;
        }

        [Authorize]
        [HttpGet("/system/{systemId}/cpu")]
        public IActionResult GetCPUs(Guid systemId)
        {
            var applicationUserSystem = context.ApplicationUserSystems.FirstOrDefault(u => u.User.Email.Equals(((ClaimsIdentity)User.Identity).FindFirst("email").Value) && u.SystemId.Equals(systemId));

            if (applicationUserSystem == null)
                return NotFound();

            var lastUpdatedCPU = context.SystemCPUs.Where(sc => sc.SystemId.Equals(systemId)).OrderByDescending(i => i.LastUpdated).FirstOrDefault();
            DateTime? lastUpdated = (lastUpdatedCPU != null ? lastUpdatedCPU.LastUpdated : (DateTime?)null);
            var CPUs = context.SystemCPUs.Where(sc => sc.SystemId.Equals(systemId) && sc.LastUpdated.Equals(lastUpdated)).ToList();

            var CPUDTOs = new List<CPUDTO>();

            foreach (var item in CPUs)
            {
                CPUDTOs.Add(CreateCPUDTO(item));
            }

            return Ok(CPUDTOs);
        }


        [Authorize]
        [HttpGet("/system/{systemId}/cpu/data")]
        public IActionResult GetCPUData(Guid systemId)
        {
            var applicationUserSystem = context.ApplicationUserSystems.FirstOrDefault(u => u.User.Email.Equals(((ClaimsIdentity) User.Identity).FindFirst("email").Value) && u.SystemId.Equals(systemId));

            if (applicationUserSystem == null)
                return NotFound();

            var CPUsData = context.SystemCPUsData.Where(sc => sc.SystemId.Equals(systemId)).OrderByDescending(i => i.CollectionDateTime).FirstOrDefault();

            if (CPUsData == null || CPUsData.CollectionDateTime.AddSeconds(9) < DateTime.Now)
            {
                return NotFound();
            }
            else
            {
                var cpuDTOs = new List<CPUDataDTO>();
                cpuDTOs.Add(CreateCPUDataDTO(CPUsData));
                return Ok(cpuDTOs);
            }
        }

        private CPUDTO CreateCPUDTO(SystemCPU systemCPU)
        {
            var cpuModel = context.CPUModels.Find(systemCPU.CPUModelID);

            var cpuDTO = new CPUDTO()
            {
                Id = systemCPU.CPUModelID,
                ClockSpeed = systemCPU.ClockSpeed,
                CoreCount = systemCPU.CoreCount,
                ThreadCount = systemCPU.ThreadCount
            };

            if (cpuModel.ModelId != null)
            {
                var model = context.Models.Find(cpuModel.ModelId);
                cpuDTO.ModelName = model.Name;
                if (model.ManufacturerId != null)
                {
                    var manufacturer = context.Manufacturers.Find(model.ManufacturerId);
                    cpuDTO.ManufacturerName = manufacturer.Name;
                }
            }

            if (cpuModel.ArchitectureId != null)
            {
                var architecture = context.Architectures.Find(cpuModel.ArchitectureId);
                cpuDTO.ArchitectureName = architecture.Name;
            }

            return cpuDTO;
        }

        private CPUDataDTO CreateCPUDataDTO(SystemCPUData systemCPUData)
        {
            return new CPUDataDTO
            {
                Processes = systemCPUData.Processes,
                Load = systemCPUData.Load,
                CollectionDateTime = systemCPUData.CollectionDateTime
            };
        }

    }
}
