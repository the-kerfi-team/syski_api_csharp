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
    public class BIOSsController : ControllerBase
    {

        private readonly SyskiDBContext context;

        public BIOSsController(SyskiDBContext context)
        {
            this.context = context;
        }

        [Authorize]
        [HttpGet("/system/{systemId}/bios")]
        public IActionResult GetBIOS(Guid systemId)
        {
            var applicationUserSystem = context.ApplicationUserSystems.FirstOrDefault(u => u.User.Email.Equals(((ClaimsIdentity) User.Identity).FindFirst("email").Value) && u.SystemId.Equals(systemId));

            if (applicationUserSystem == null)
                return NotFound();

            var bios = context.SystemBIOSs.FirstOrDefault(sc => sc.SystemId.Equals(systemId));
            var biosDTO = CreateBIOSDTO(bios);

            return Ok(biosDTO);
        }

        private BIOSDTO CreateBIOSDTO(SystemBIOS systemBIOS)
        {
            var biosModel = context.BIOSModels.Find(systemBIOS.BIOSModelId);

            var biosDTO = new BIOSDTO()
            {
                Id = systemBIOS.BIOSModelId,
                Caption = systemBIOS.Caption,
                Date = systemBIOS.Date,
                Version = systemBIOS.Version
            };

            if (biosModel.ManufacturerId != null)
            {
                var manufacturer = context.Manufacturers.Find(biosModel.ManufacturerId);
                biosDTO.ManufacturerName = manufacturer.Name;
            }

            return biosDTO;
        }

    }
}
