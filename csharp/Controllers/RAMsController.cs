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
    public class RAMsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RAMsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{systemId}")]
        public async Task<IActionResult> GetRAMs(Guid systemId)
        {
            if (_context.Systems.Find(systemId) == null)
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
            MemoryModel MemoryModel = _context.MemoryModels.Find(systemRAM.RAMModelId);
            Model Model = _context.Models.Find(systemRAM.RAMModelId);
            Manufacturer Manufacturer = _context.Manufacturers.Find(Model.ManufacturerId);
            MemoryType MemoryType = _context.MemoryTypes.Find(MemoryModel.MemoryTypeId);

            RAMDTO ramDTO = new RAMDTO()
            {
                Id = systemRAM.RAMModelId,
                ModelName = Model.Name,
                ManufacturerName = Manufacturer.Name,
                MemoryTypeName = MemoryType.Name,
                MemoryBytes = MemoryModel.MemoryBytes
            };

            return ramDTO;
        }
    }    
}