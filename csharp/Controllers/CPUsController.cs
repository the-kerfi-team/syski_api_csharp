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
    public class CPUsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CPUsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{systemId}")]
        public async Task<IActionResult> GetCPUs (Guid systemId)
        {
            if (_context.Systems.Find(systemId) == null)
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
            var processorModel = _context.ProcessorModels.Find(systemCPU.CPUModelID);
            var architecture = _context.Architectures.Find(processorModel.ArchitectureId);
            var model = _context.Models.Find(processorModel.Id);
            var manufacturer = _context.Manufacturers.Find(model.ManufacturerId);

            var CPUDTO = new CPUDTO()
            {
                Id = systemCPU.CPUModelID,
                ModelName = model.Name,
                ManufacturerName = manufacturer.Name,
                ArchitectureName = architecture.Name,
                ClockSpeed = processorModel.ClockSpeed,
                CoreCount = processorModel.CoreCount,
                ThreadCount = processorModel.ThreadCount
            };

            return CPUDTO;
        }
    }
}