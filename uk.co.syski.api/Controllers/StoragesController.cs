using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Syski.API.Models;
using Syski.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Syski.API.Controllers
{
    [ApiController]
    public class StoragesController : ControllerBase
    {

        private readonly SyskiDBContext context;

        public StoragesController(SyskiDBContext context)
        {
            this.context = context;
        }

        [Authorize]
        [HttpGet("/system/{systemId}/storage")]
        public IActionResult GetStorages(Guid systemId)
        {
            var applicationUserSystem = context.ApplicationUserSystems.FirstOrDefault(u => u.User.Email.Equals(((ClaimsIdentity) User.Identity).FindFirst("email").Value) && u.SystemId.Equals(systemId));

            if (applicationUserSystem == null)
                return NotFound();

            var lastUpdatedStorage = context.SystemStorages.Where(sc => sc.SystemId.Equals(systemId)).OrderByDescending(i => i.LastUpdated).FirstOrDefault();
            DateTime? lastUpdated = (lastUpdatedStorage != null ? lastUpdatedStorage.LastUpdated : (DateTime?) null);
            var Storages = context.SystemStorages.Where(sc => sc.SystemId.Equals(systemId) && sc.LastUpdated.Equals(lastUpdated)).ToList();

            var StorageDTOs = new List<StorageDTO>();

            foreach (var item in Storages)
            {
                StorageDTOs.Add(CreateStorageDTO(item));
            }

            return Ok(StorageDTOs);
        }

        [Authorize]
        [HttpGet("/system/{systemId}/storage/data")]
        public IActionResult GetStorageData(Guid systemId)
        {
            var applicationUserSystem = context.ApplicationUserSystems.FirstOrDefault(u => u.User.Email.Equals(((ClaimsIdentity)User.Identity).FindFirst("email").Value) && u.SystemId.Equals(systemId));

            if (applicationUserSystem == null)
                return NotFound();

            var storageData = context.SystemStorageData.Where(sc => sc.SystemId.Equals(systemId)).OrderByDescending(i => i.CollectionDateTime).FirstOrDefault();

            if (storageData == null || storageData.CollectionDateTime.AddSeconds(9) < DateTime.Now)
            {
                return NotFound();
            }
            else
            {
                var storageDTOs = new List<StorageDataDTO>();
                storageDTOs.Add(CreateStorageDataDTO(storageData));
                return Ok(storageDTOs);
            }
        }

        private StorageDTO CreateStorageDTO(SystemStorage systemStorage)
        {
            StorageModel storageModel = context.StorageModels.Find(systemStorage.StorageModelId);

            StorageDTO storageDTO = new StorageDTO()
            {
                Id = storageModel.Id,
                MemoryBytes = storageModel.Size
            };

            if (storageModel.ModelId != null)
            {
                var model = context.Models.Find(storageModel.ModelId);
                storageDTO.ModelName = model.Name;
                if (model.ManufacturerId != null)
                {
                    var manufacturer = context.Manufacturers.Find(model.ManufacturerId);
                    storageDTO.ManufacturerName = manufacturer.Name;
                }
            }

            if (systemStorage.StorageInterfaceId != null)
            {
                StorageInterfaceType StorageType = context.StorageInterfaceTypes.Find(systemStorage.StorageInterfaceId);
                storageDTO.MemoryTypeName = (StorageType != null ? StorageType.Name : null);
            }

            return storageDTO;
        }

        private StorageDataDTO CreateStorageDataDTO(SystemStorageData systemStorageData)
        {
            return new StorageDataDTO
            {
                ByteReads = systemStorageData.ByteReads,
                ByteWrites = systemStorageData.ByteWrites,
                Reads = systemStorageData.Reads,
                Writes = systemStorageData.Writes,
                Time = systemStorageData.Time,
                Transfers = systemStorageData.Transfers,
                CollectionDateTime = systemStorageData.CollectionDateTime
            };
        }

    }
}
