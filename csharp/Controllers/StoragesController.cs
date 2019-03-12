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
    public class StoragesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StoragesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("/system/{systemId}/storage")]
        public async Task<IActionResult> GetStorages(Guid systemId)
        {
            var applicationUserSystem = _context.ApplicationUserSystems
                .Where(u => u.User.Email == ((ClaimsIdentity)User.Identity).FindFirst("email").Value && u.SystemId == systemId).FirstOrDefault();

            if (applicationUserSystem == null)
                return NotFound();

            DateTime lastUpdated = _context.SystemStorages.Where(sc => sc.SystemId == systemId).OrderByDescending(i => i.LastUpdated).FirstOrDefault().LastUpdated;
            var Storages = _context.SystemStorages.Where(sc => sc.SystemId == systemId && sc.LastUpdated == lastUpdated).ToList();

            var StorageDTOs = new List<StorageDTO>();

            foreach (var item in Storages)
            {
                StorageDTOs.Add(CreateDTO(item));
            }

            return Ok(StorageDTOs);
        }

        [Authorize]
        [HttpPost("/system/{systemId}/storage/data")]
        public async Task<IActionResult> GetStorageData(Guid systemId)
        {
            var applicationUserSystem = _context.ApplicationUserSystems
                .Where(u => u.User.Email == ((ClaimsIdentity)User.Identity).FindFirst("email").Value && u.SystemId == systemId).FirstOrDefault();

            if (applicationUserSystem == null)
                return NotFound();

            var CPUsData = _context.SystemCPUsData.Where(sc => sc.SystemId == systemId).OrderByDescending(i => i.CollectionDateTime).Take(1);

            return Ok(CPUsData);
        }

        private StorageDTO CreateDTO(SystemStorage systemStorage)
        {
            StorageModel MemoryModel = _context.StorageModels.Find(systemStorage.StorageModelId);
            Model Model = _context.Models.Find(MemoryModel.ModelId);
            Manufacturer Manufacturer = _context.Manufacturers.Find(Model.ManufacturerId);
            StorageType StorageType = _context.StorageTypes.Find(systemStorage.TypeId);

            StorageDTO storageDTO = new StorageDTO()
            {
                Id = systemStorage.StorageModelId,
                ModelName = Model.Name,
                ManufacturerName = Manufacturer.Name,
                MemoryTypeName = (StorageType != null ? StorageType.Name : null),
                MemoryBytes = MemoryModel.Size
            };

            return storageDTO;
        }
    }
}