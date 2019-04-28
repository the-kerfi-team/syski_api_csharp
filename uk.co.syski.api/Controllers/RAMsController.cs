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
    public class RAMsController : ControllerBase
    {

        private readonly SyskiDBContext context;

        public RAMsController(SyskiDBContext context)
        {
            this.context = context;
        }

        [Authorize]
        [HttpGet("/system/{systemId}/ram")]
        public IActionResult GetRAMs(Guid systemId)
        {
            var applicationUserSystem = context.ApplicationUserSystems.FirstOrDefault(u => u.User.Email.Equals(((ClaimsIdentity) User.Identity).FindFirst("email").Value) && u.SystemId.Equals(systemId));

            if (applicationUserSystem == null)
                return NotFound();

            var lastUpdatedRAM = context.SystemRAMs.Where(sc => sc.SystemId.Equals(systemId)).OrderByDescending(i => i.LastUpdated).FirstOrDefault();
            DateTime? lastUpdated = (lastUpdatedRAM != null ? lastUpdatedRAM.LastUpdated : (DateTime?) null);
            var RAMs = context.SystemRAMs.Where(sc => sc.SystemId.Equals(systemId) && sc.LastUpdated.Equals(lastUpdated)).ToList();

            var RAMDTOs = new List<RAMDTO>();

            foreach (var item in RAMs)
            {
                RAMDTOs.Add(CreateRAMDTO(item));
            }

            return Ok(RAMDTOs);
        }

        [Authorize]
        [HttpGet("/system/{systemId}/ram/data")]
        public IActionResult GetRAMData(Guid systemId)
        {
            var applicationUserSystem = context.ApplicationUserSystems
                .Where(u => u.User.Email == ((ClaimsIdentity)User.Identity).FindFirst("email").Value && u.SystemId == systemId).FirstOrDefault();

            if (applicationUserSystem == null)
                return NotFound();

            var ramData = context.SystemRAMData.Where(sc => sc.SystemId == systemId).OrderByDescending(i => i.CollectionDateTime).FirstOrDefault();

            if (ramData == null || ramData.CollectionDateTime.AddSeconds(9) < DateTime.Now)
            {
                return NotFound();
            }
            else
            {
                var ramDTOs = new List<RAMDataDTO>();
                ramDTOs.Add(CreateRAMDataDTO(ramData));
                return Ok(ramDTOs);
            }
        }

        private RAMDTO CreateRAMDTO(SystemRAM systemRAM)
        {
            RAMModel ramModel = context.RAMModels.Find(systemRAM.RAMModelId);

            RAMDTO ramDTO = new RAMDTO()
            {
                Id = ramModel.Id,
                MemoryBytes = ramModel.Size
            };

            if (ramModel.ModelId != null)
            {
                var model = context.Models.Find(ramModel.ModelId);
                ramDTO.ModelName = model.Name;
                if (model.ManufacturerId != null)
                {
                    var manufacturer = context.Manufacturers.Find(model.ManufacturerId);
                    ramDTO.ManufacturerName = manufacturer.Name;
                }
            }

            return ramDTO;
        }

        private RAMDataDTO CreateRAMDataDTO(SystemRAMData systemRAMData)
        {
            return new RAMDataDTO
            {
                Free = systemRAMData.Free,
                CollectionDateTime = systemRAMData.CollectionDateTime
            };
        }

    }
}
