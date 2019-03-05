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
            var Motherboard = _context.SystemMotherboards.Find(System.Id);
            var MotherboardDTO = CreateDTO(Motherboard);

            return Ok(MotherboardDTO);
        }

        private MotherboardDTO CreateDTO(SystemMotherboard systemMotherboard)
        {
            var MotherboardModel = _context.MotherboardModels.Find(systemMotherboard.MotherboardModelId);
            var Model = _context.Models.Find(MotherboardModel.ModelId);
            var Manufacturer = _context.Manufacturers.Find(Model.ManufacturerId);

            var MotherboardDTO = new MotherboardDTO()
            {
                Id = MotherboardModel.Id,
                ModelName = Model.Name,
                ManufacturerName = Manufacturer.Name,
                //Version = motherboardModel.Version
            };

            return MotherboardDTO;
        }
    }
}