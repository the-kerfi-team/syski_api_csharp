﻿using System;
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
    public class SystemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SystemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetSystemsIndex()
        {
            var systems = _context.Systems.ToList();

            List<SystemDTO> systemDTOs = new List<SystemDTO>();

            foreach (var item in systems)
            {
                systemDTOs.Add(CreateDTO(item));
            }

            return Ok(systemDTOs);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetSystem(Guid Id)
        {
            if (await _context.Systems.FindAsync(Id) == null)
                return NotFound();

            return Ok(CreateDTO(await _context.Systems.FindAsync(Id)));
        }

        private SystemDTO CreateDTO(csharp.Data.System item)
        {
            var systemModel = _context.SystemModels.Find(item.ModelId);           
            var model = _context.Models.Find(systemModel.Id);
            var manufacturer = _context.Manufacturers.Find(model.ManufacturerId);

            var systemModelTypes =  _context.SystemModelTypes
                                            .Where(smt => smt.SystemModelId == systemModel.Id)
                                            .ToList();

            var types = new List<Data.Type>();
            foreach(var systemModelType in systemModelTypes)
            {
                types.Add(_context.Types.Find(systemModelType.TypeId));
            }

            var systemDTO = new SystemDTO()
            {
                Id = item.Id,
                ModelName = model.Name,
                ManufacturerName = manufacturer.Name,
                Types = types,
                HostName = item.HostName,
                LastUpdated = item.LastUpdated
            };

            return systemDTO;
        }

    }

}