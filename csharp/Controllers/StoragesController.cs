﻿using System;
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
    public class StoragesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StoragesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("{systemId}")]
        public async Task<IActionResult> GetStorages(Guid systemId)
        {
            var applicationUserSystem = _context.ApplicationUserSystems
                .Where(u => u.User.Email == ((ClaimsIdentity)User.Identity).FindFirst("email").Value && u.SystemId == systemId).FirstOrDefault();

            if (applicationUserSystem == null)
                return NotFound();

            var Storages = _context.SystemStorages.Where(sc => sc.SystemId == systemId).ToList();

            var StorageDTOs = new List<StorageDTO>();

            foreach (var item in Storages)
            {
                StorageDTOs.Add(CreateDTO(item));
            }

            return Ok(StorageDTOs);
        }

        private StorageDTO CreateDTO(SystemStorage systemStorage)
        {
            MemoryModel MemoryModel = _context.MemoryModels.Find(systemStorage.StorageModelId);
            Model Model = _context.Models.Find(systemStorage.StorageModelId);
            Manufacturer Manufacturer = _context.Manufacturers.Find(Model.ManufacturerId);
            MemoryType MemoryType = _context.MemoryTypes.Find(MemoryModel.MemoryTypeId);

            StorageDTO storageDTO = new StorageDTO()
            {
                Id = systemStorage.StorageModelId,
                ModelName = Model.Name,
                ManufacturerName = Manufacturer.Name,
                MemoryTypeName = MemoryType.Name,
                MemoryBytes = MemoryModel.MemoryBytes
            };

            return storageDTO;
        }
    }
}