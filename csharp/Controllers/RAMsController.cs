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
    public class RAMsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RAMsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("{systemId}")]
        public async Task<IActionResult> GetRAMs(Guid systemId)
        {
            var applicationUserSystem = _context.ApplicationUserSystems
                .Where(u => u.User.Email == ((ClaimsIdentity)User.Identity).FindFirst("email").Value && u.SystemId == systemId).FirstOrDefault();

            if (applicationUserSystem == null)
                return NotFound();

            var RAMs = _context.SystemRAMs.Where(sc => sc.SystemId == systemId).ToList();

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
            Model Model = _context.Models.Find(systemRAM.RAMModelId);
            Manufacturer Manufacturer = _context.Manufacturers.Find(Model.ManufacturerId);
            StorageType StorageType = _context.StorageTypes.Find(systemRAM.TypeId);

            RAMDTO ramDTO = new RAMDTO()
            {
                Id = systemRAM.RAMModelId,
                ModelName = Model.Name,
                ManufacturerName = Manufacturer.Name,
                MemoryTypeName = StorageType.Name,
                MemoryBytes = MemoryModel.Size
            };

            return ramDTO;
        }
    }    
}