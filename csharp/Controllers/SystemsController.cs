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
    public class SystemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SystemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("/system")]
        public IActionResult GetSystemsIndex()
        {
            var applicationUserSystems = _context.ApplicationUserSystems.Where(u => u.User.Email == ((ClaimsIdentity)User.Identity).FindFirst("email").Value).ToList();
            List<SystemDTO> systemDTOs = new List<SystemDTO>();
            foreach (var item in applicationUserSystems)
            {
                systemDTOs.Add(CreateDTO(_context.Systems.First(s => s.Id == item.SystemId)));
            }
            return Ok(systemDTOs);
        }

        private SystemDTO CreateDTO(csharp.Data.System item)
        {
            var systemDTO = new SystemDTO()
            {
                Id = item.Id,
                HostName = item.HostName,
                LastUpdated = item.LastUpdated
            };
            if (item.ModelId != null)
            {
                var model = _context.Models.Find(item.ModelId);
                var manufacturer = _context.Manufacturers.Find(model.ManufacturerId);

                var systemTypes = _context.SystemTypes
                                                .Where(smt => smt.SystemId == item.ModelId)
                                                .ToList();
                var types = new List<Data.Type>();
                foreach (var systemModelType in systemTypes)
                {
                    types.Add(_context.Types.Find(systemModelType.TypeId));
                }

                systemDTO.ModelName = model.Name;
                systemDTO.ManufacturerName = manufacturer.Name;
                systemDTO.Types = types;
            }
            return systemDTO;
        }

    }

}