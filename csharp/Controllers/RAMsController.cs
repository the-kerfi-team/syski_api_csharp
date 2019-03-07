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
    public class RAMsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RAMsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("/system/{systemId}/ram")]
        public async Task<IActionResult> GetRAMs(Guid systemId)
        {
            var applicationUserSystem = _context.ApplicationUserSystems
                .Where(u => u.User.Email == ((ClaimsIdentity)User.Identity).FindFirst("email").Value && u.SystemId == systemId).FirstOrDefault();

            if (applicationUserSystem == null)
                return NotFound();

            DateTime lastUpdated = _context.SystemRAMs.Where(sc => sc.SystemId == systemId).OrderByDescending(i => i.LastUpdated).FirstOrDefault().LastUpdated;
            var RAMs = _context.SystemRAMs.Where(sc => sc.SystemId == systemId && sc.LastUpdated == lastUpdated).ToList();

            var RAMDTOs = new List<RAMDTO>();

            foreach (var item in RAMs)
            {
                RAMDTOs.Add(CreateDTO(item));
            }

            return Ok(RAMDTOs);
        }

        private RAMDTO CreateDTO(SystemRAM systemRAM)
        {
            RAMModel MemoryModel = _context.RAMModels.Find(systemRAM.RAMModelId);
            Model Model = _context.Models.Find(MemoryModel.ModelId);
            Manufacturer Manufacturer = _context.Manufacturers.Find(Model.ManufacturerId);
            StorageType StorageType = _context.StorageTypes.Find(systemRAM.TypeId);

            RAMDTO ramDTO = new RAMDTO()
            {
                Id = systemRAM.RAMModelId,
                ModelName = Model.Name,
                ManufacturerName = Manufacturer.Name,
                MemoryTypeName = (StorageType != null ? StorageType.Name : null),
                MemoryBytes = MemoryModel.Size
            };

            return ramDTO;
        }
    }    
}