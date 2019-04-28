using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Syski.API.Models;
using Syski.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Syski.API.Controllers
{
    [ApiController]
    public class SystemsController : ControllerBase
    {

        private readonly SyskiDBContext context;

        public SystemsController(SyskiDBContext context)
        {
            this.context = context;
        }

        [Authorize]
        [HttpGet("/system/all")]
        public IActionResult GetSystemsIndex()
        {
            var applicationUserSystems = context.ApplicationUserSystems.Where(u => u.User.Email.Equals(((ClaimsIdentity) User.Identity).FindFirst("email").Value)).ToList();

            List<SystemDTO> systemDTOs = new List<SystemDTO>();
            foreach (var item in applicationUserSystems)
            {
                systemDTOs.Add(CreateSystemDTO(context.Systems.First(s => s.Id == item.SystemId)));
            }
            return Ok(systemDTOs);
        }

        [Authorize]
        [HttpGet("/system/{systemId}/ping")]
        public IActionResult GetPing(Guid systemId)
        {
            var applicationUserSystem = context.ApplicationUserSystems.FirstOrDefault(u => u.User.Email.Equals(((ClaimsIdentity)User.Identity).FindFirst("email").Value) && u.SystemId.Equals(systemId));

            if (applicationUserSystem == null)
                return NotFound();

            var pingData = context.SystemPingData.Where(sc => sc.SystemId.Equals(systemId)).OrderByDescending(i => i.CollectionDateTime).FirstOrDefault();

            if (pingData == null || pingData.CollectionDateTime.AddSeconds(30) < DateTime.Now)
            {
                return NotFound();
            }
            else
            {
                return Ok(new SystemPingDTO
                {
                    ping = pingData.CollectionDateTime.Subtract(pingData.SendPingTime).TotalMilliseconds
                });
            }
        }

        [Authorize]
        [HttpPost("/system/{systemId}/restart")]
        public IActionResult RestartSystem(Guid systemId)
        {
            var applicationUserSystem = context.ApplicationUserSystems.FirstOrDefault(u => u.User.Email.Equals(((ClaimsIdentity)User.Identity).FindFirst("email").Value) && u.SystemId.Equals(systemId));

            if (applicationUserSystem == null)
                return NotFound();

            context.Add(new SystemCommand
            {
                SystemId = systemId,
                Action = "restart",
                QueuedTime = DateTime.Now
            });
            context.SaveChanges();

            return Ok();
        }

        [Authorize]
        [HttpPost("/system/{systemId}/shutdown")]
        public IActionResult ShutdownSystem(Guid systemId)
        {
            var applicationUserSystem = context.ApplicationUserSystems.FirstOrDefault(u => u.User.Email.Equals(((ClaimsIdentity)User.Identity).FindFirst("email").Value) && u.SystemId.Equals(systemId));

            if (applicationUserSystem == null)
                return NotFound();

            context.Add(new SystemCommand
            {
                SystemId = systemId,
                Action = "shutdown",
                QueuedTime = DateTime.Now
            });
            context.SaveChanges();

            return Ok();
        }

        [Authorize]
        [HttpPost("/system/{systemId}/remove")]
        public IActionResult RemoveSystem(Guid systemId)
        {
            var applicationUserSystem = context.ApplicationUserSystems.FirstOrDefault(u => u.User.Email.Equals(((ClaimsIdentity)User.Identity).FindFirst("email").Value) && u.SystemId.Equals(systemId));

            if (applicationUserSystem == null)
                return NotFound();

            context.Remove(applicationUserSystem);
            context.SaveChanges();

            return Ok();
        }

        [Authorize]
        [HttpPost("/system/{systemId}/processes/kill")]
        public IActionResult KillProcess(Guid systemId, [FromBody] KillProcessDTO killProcessDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicationUserSystem = context.ApplicationUserSystems.FirstOrDefault(u => u.User.Email.Equals(((ClaimsIdentity)User.Identity).FindFirst("email").Value) && u.SystemId.Equals(systemId));

            if (applicationUserSystem == null)
                return NotFound();

            JObject properties = new JObject { { "id", killProcessDTO.Id } };
            context.Add(new SystemCommand
            {
                SystemId = systemId,
                Action = "killprocess",
                Properties = properties.ToString(),
                QueuedTime = DateTime.Now
            });
            context.SaveChanges();

            return Ok();
        }

        [Authorize]
        [HttpGet("/system/{systemId}/processes")]
        public IActionResult GetProcesses(Guid systemId)
        {
            var applicationUserSystem = context.ApplicationUserSystems.FirstOrDefault(u => u.User.Email.Equals(((ClaimsIdentity) User.Identity).FindFirst("email").Value) && u.SystemId.Equals(systemId));

            if (applicationUserSystem == null)
                return NotFound();

            var processesData = context.SystemRunningProcesses.Where(sc => sc.SystemId.Equals(systemId)).OrderByDescending(i => i.CollectionDateTime).FirstOrDefault();
            if (processesData == null || processesData.CollectionDateTime.AddSeconds(30) <= DateTime.Now)
            {
                return NotFound();
            }
            else
            {
                var runningProcessesDTOs = new List<RunningProcessesDTO>();
                var processesList = context.SystemRunningProcesses.Where(sc => sc.SystemId.Equals(systemId) && sc.CollectionDateTime.Equals(processesData.CollectionDateTime)).ToList();
                foreach (var process in processesList)
                {
                    runningProcessesDTOs.Add(new RunningProcessesDTO
                    {
                        Id = process.Id,
                        CollectionDateTime = process.CollectionDateTime,
                        KernelTime = process.KernelTime,
                        MemSize = process.MemSize,
                        Name = process.Name,
                        ParentId = process.ParentId,
                        Path = process.Path,
                        Threads = process.Threads,
                        UpTime = process.UpTime
                    });
                }
                return Ok(runningProcessesDTOs);
            }
        }

        private SystemDTO CreateSystemDTO(Data.System item)
        {
            var systemDTO = new SystemDTO()
            {
                Id = item.Id,
                HostName = item.HostName,
                LastUpdated = item.LastUpdated
            };

            if (item.ModelId != null)
            {
                var model = context.Models.Find(item.ModelId);
                systemDTO.ModelName = model.Name;
                if (model.ManufacturerId != null)
                {
                    var manufacturer = context.Manufacturers.Find(model.ManufacturerId);
                    systemDTO.ManufacturerName = manufacturer.Name;
                }
            }

            var systemTypes = context.SystemTypes.Where(smt => smt.SystemId == item.Id).ToList();
            var types = new List<string>();
            foreach (var systemType in systemTypes)
            {
                var systemTypeName = context.SystemTypeNames.Find(systemType.TypeId);
                types.Add(systemTypeName.Name);
            }
            systemDTO.SystemTypes = types;

            return systemDTO;
        }

    }
}
