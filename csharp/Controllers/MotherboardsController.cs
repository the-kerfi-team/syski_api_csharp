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
    public class MotherboardsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MotherboardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("{systemId}")]
        public async Task<IActionResult> GetMotherboard(Guid systemId)
        {
            var applicationUserSystem = _context.ApplicationUserSystems
                .Where(u => u.User.Email == ((ClaimsIdentity)User.Identity).FindFirst("email").Value && u.SystemId == systemId).FirstOrDefault();

            if (applicationUserSystem == null)
                return NotFound();

            var System = _context.Systems.Find(systemId);
            var Motherboard = _context.MotherboardModels.Find(System.MotherboardId);
            var MotherboardDTO = CreateDTO(Motherboard);

            return Ok(MotherboardDTO);
        }

        private MotherboardDTO CreateDTO(MotherboardModel motherboardModel)
        {
            var Model = _context.Models.Find(motherboardModel.Id);
            var Manufacturer = _context.Manufacturers.Find(Model.ManufacturerId);

            var MotherboardDTO = new MotherboardDTO()
            {
                Id = motherboardModel.Id,
                ModelName = Model.Name,
                ManufacturerName = Manufacturer.Name,
                SerialNumber = motherboardModel.SerialNumber,
                Version = motherboardModel.Version
            };

            return MotherboardDTO;
        }
    }
}