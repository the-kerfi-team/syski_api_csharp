using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Syski.API.Models;
using Syski.Data;
using System;
using System.Linq;
using System.Security.Claims;

namespace Syski.API.Controllers
{
    [ApiController]
    public class MotherboardsController : ControllerBase
    {

        private readonly SyskiDBContext context;

        public MotherboardsController(SyskiDBContext context)
        {
            this.context = context;
        }

        [Authorize]
        [HttpGet("/system/{systemId}/motherboard")]
        public IActionResult GetMotherboard(Guid systemId)
        {
            var applicationUserSystem = context.ApplicationUserSystems.FirstOrDefault(u => u.User.Email.Equals(((ClaimsIdentity) User.Identity).FindFirst("email").Value) && u.SystemId.Equals(systemId));

            if (applicationUserSystem == null)
                return NotFound();

            var motherboard = context.SystemMotherboards.FirstOrDefault(sc => sc.SystemId.Equals(systemId));
            var MotherboardDTO = CreateMotherboardDTO(motherboard);

            return Ok(MotherboardDTO);
        }

        private MotherboardDTO CreateMotherboardDTO(SystemMotherboard systemMotherboard)
        {
            if (systemMotherboard != null)
            {
                var motherboardModel = context.MotherboardModels.Find(systemMotherboard.MotherboardModelId);

                var motherboardDTO = new MotherboardDTO()
                {
                    Id = systemMotherboard.MotherboardModelId,
                    Version = motherboardModel.Version
                };

                if (motherboardModel.ModelId != null)
                {
                    var model = context.Models.Find(motherboardModel.ModelId);
                    motherboardDTO.ModelName = model.Name;
                    if (model.ManufacturerId != null)
                    {
                        var manufacturer = context.Manufacturers.Find(model.ManufacturerId);
                        motherboardDTO.ManufacturerName = manufacturer.Name;
                    }
                }

                return motherboardDTO;
            }
            else
            {
                return new MotherboardDTO();
            }
        }

    }
}
