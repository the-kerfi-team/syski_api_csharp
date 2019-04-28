using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Syski.Data
{
    public class SyskiDBContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<AuthenticationToken> AuthenticationTokens { get; set; }
        public DbSet<ApplicationUserSystems> ApplicationUserSystems { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<Model> Models { get; set; }

        public DbSet<System> Systems { get; set; }

        public DbSet<SystemType> SystemTypes { get; set; }
        public DbSet<SystemTypeName> SystemTypeNames { get; set; }

        public DbSet<SystemCPU> SystemCPUs { get; set; }
        public DbSet<CPUModel> CPUModels { get; set; }

        public DbSet<SystemRAM> SystemRAMs { get; set; }
        public DbSet<RAMModel> RAMModels { get; set; }

        public DbSet<SystemGPU> SystemGPUs { get; set; }
        public DbSet<GPUModel> GPUModels { get; set; }

        public DbSet<SystemStorage> SystemStorages { get; set; }
        public DbSet<StorageModel> StorageModels { get; set; }
        public DbSet<StorageInterfaceType> StorageInterfaceTypes { get; set; }

        public DbSet<SystemMotherboard> SystemMotherboards { get; set; }
        public DbSet<MotherboardModel> MotherboardModels { get; set; }

        public DbSet<SystemBIOS> SystemBIOSs { get; set; }
        public DbSet<BIOSModel> BIOSModels { get; set; }

        public DbSet<SystemOS> SystemOSs { get; set; }
        public DbSet<OperatingSystemModel> OperatingSystemModels { get; set; }

        public DbSet<Architecture> Architectures { get; set; }

        public DbSet<SystemPingData> SystemPingData { get; set; }

        public DbSet<SystemCPUData> SystemCPUsData { get; set; }
        public DbSet<SystemRAMData> SystemRAMData { get; set; }
        public DbSet<SystemStorageData> SystemStorageData { get; set; }

        public DbSet<SystemRunningProcesses> SystemRunningProcesses { get; set; }

        public DbSet<SystemCommand> SystemCommands { get; set; }

        public SyskiDBContext(DbContextOptions<SyskiDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var authenticationTokenEntity = builder.Entity<AuthenticationToken>();

            authenticationTokenEntity.HasOne(t => t.User)
                .WithMany(u => u.AuthenticationTokens)
                .HasForeignKey(f => f.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            //authenticationTokenEntity.HasOne(t => t.NextToken)
            //    .WithOne(t => t.PreviousToken)
            //    .HasForeignKey<Token>(t => t.NextTokenId);

            //authenticationTokenEntity.HasOne(t => t.PreviousToken)
            //    .WithOne(t => t.NextToken)
            //    .HasForeignKey<Token>(t => t.PreviousTokenId);

            var applicationUserSystemsEntity = builder.Entity<ApplicationUserSystems>();

            applicationUserSystemsEntity.HasOne(aus => aus.System)
                .WithMany(s => s.Users)
                .HasForeignKey(aus => aus.SystemId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            applicationUserSystemsEntity.HasOne(aus => aus.User)
                .WithMany(au => au.Systems)
                .HasForeignKey(aus => aus.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            applicationUserSystemsEntity.HasOne(aus => aus.Category)
                .WithMany(ausc => ausc.Systems)
                .HasForeignKey(aus => aus.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            applicationUserSystemsEntity.HasKey(aus => new { aus.UserId, aus.SystemId });

            var manufacturerEntity = builder.Entity<Manufacturer>();

            manufacturerEntity.HasMany(m => m.Models)
                .WithOne(m => m.Manufacturer)
                .HasForeignKey(m => m.ManufacturerId)
                .OnDelete(DeleteBehavior.Restrict);

            var systemEntity = builder.Entity<System>();

            systemEntity.HasOne(s => s.Model)
                .WithMany(m => m.Systems)
                .HasForeignKey(s => s.ModelId)
                .OnDelete(DeleteBehavior.Restrict);

            var systemTypeEntity = builder.Entity<SystemType>();

            systemTypeEntity.HasOne(st => st.System)
                .WithMany(s => s.SystemTypes)
                .HasForeignKey(st => st.SystemId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            systemTypeEntity.HasOne(st => st.Type)
                .WithMany(stn => stn.SystemTypes)
                .HasForeignKey(st => st.TypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            systemTypeEntity.HasKey(smt => new { smt.SystemId, smt.TypeId });

            var systemCPUEntity = builder.Entity<SystemCPU>();

            systemCPUEntity.HasOne(scpu => scpu.System)
                .WithMany(s => s.SystemCPUs)
                .HasForeignKey(scpu => scpu.SystemId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            systemCPUEntity.HasOne(scpu => scpu.CPUModel)
                .WithMany(cpum => cpum.SystemCPUs)
                .HasForeignKey(scpu => scpu.CPUModelID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            systemCPUEntity.HasKey(sc => new { sc.SystemId, sc.CPUModelID, sc.Slot });

            var cpuModelEntity = builder.Entity<CPUModel>();

            cpuModelEntity.HasOne(cpum => cpum.Architecture)
                .WithMany(a => a.CPUModels)
                .HasForeignKey(cpum => cpum.ArchitectureId)
                .OnDelete(DeleteBehavior.Restrict);

            cpuModelEntity.HasOne(cpum => cpum.Model)
                .WithOne(m => m.CPUModel)
                .HasForeignKey<CPUModel>(cpum => cpum.ModelId)
                .OnDelete(DeleteBehavior.Restrict);

            var systemRAMEntity = builder.Entity<SystemRAM>();

            systemRAMEntity.HasOne(sram => sram.System)
                .WithMany(s => s.SystemRAMs)
                .HasForeignKey(sram => sram.SystemId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            systemRAMEntity.HasOne(sram => sram.RAMModel)
                .WithMany(ramm => ramm.SystemRAMs)
                .HasForeignKey(sram => sram.RAMModelId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            systemRAMEntity.HasKey(sr => new { sr.SystemId, sr.RAMModelId, sr.Slot });

            var ramModelEntity = builder.Entity<RAMModel>();

            ramModelEntity.HasOne(ramm => ramm.Model)
                .WithOne(m => m.RAMModel)
                .HasForeignKey<RAMModel>(ramm => ramm.ModelId)
                .OnDelete(DeleteBehavior.Restrict);

            var systemGPUEntity = builder.Entity<SystemGPU>();

            systemGPUEntity.HasOne(sgpu => sgpu.System)
                .WithMany(s => s.SystemGPUs)
                .HasForeignKey(sgpu => sgpu.SystemId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            systemGPUEntity.HasOne(sgpu => sgpu.GPUModel)
                .WithMany(gpum => gpum.SystemGPUs)
                .HasForeignKey(sgpu => sgpu.GPUModelId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            systemGPUEntity.HasKey(sg => new { sg.SystemId, sg.GPUModelId, sg.Slot });

            var gpuModelEntity = builder.Entity<GPUModel>();

            gpuModelEntity.HasOne(gpum => gpum.Model)
                .WithOne(m => m.GPUModel)
                .HasForeignKey<GPUModel>(gpum => gpum.ModelId)
                .OnDelete(DeleteBehavior.Restrict);

            var systemStorage = builder.Entity<SystemStorage>();

            systemStorage.HasOne(ss => ss.System)
                .WithMany(s => s.SystemStorages)
                .HasForeignKey(ss => ss.SystemId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            systemStorage.HasOne(ss => ss.StorageModel)
                .WithMany(sm => sm.SystemStorages)
                .HasForeignKey(ss => ss.StorageModelId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            systemStorage.HasOne(ss => ss.StorageInterface)
                .WithMany(sit => sit.SystemStorages)
                .HasForeignKey(ss => ss.StorageInterfaceId)
                .OnDelete(DeleteBehavior.Restrict);

            systemStorage.HasKey(ss => new { ss.SystemId, ss.StorageModelId, ss.Slot });

            var storageModelEntity = builder.Entity<StorageModel>();

            storageModelEntity.HasOne(sm => sm.Model)
                .WithOne(m => m.StorageModel)
                .HasForeignKey<StorageModel>(sm => sm.ModelId)
                .OnDelete(DeleteBehavior.Restrict);

            var systemMotherboard = builder.Entity<SystemMotherboard>();

            systemMotherboard.HasOne(sm => sm.System)
                .WithOne(sm => sm.SystemMotherboard)
                .HasForeignKey<SystemMotherboard>(sm => sm.SystemId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            systemMotherboard.HasOne(sm => sm.MotherboardModel)
                .WithMany(sm => sm.SystemMotherboards)
                .HasForeignKey(sm => sm.MotherboardModelId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            systemMotherboard.HasKey(sc => new { sc.SystemId });

            var motherboardModel = builder.Entity<MotherboardModel>();

            motherboardModel.HasOne(mm => mm.Model)
                .WithOne(m => m.MotherboardModel)
                .HasForeignKey<MotherboardModel>(mm => mm.ModelId)
                .OnDelete(DeleteBehavior.Restrict);

            var systemBIOS = builder.Entity<SystemBIOS>();

            systemBIOS.HasOne(sb => sb.System)
                .WithOne(s => s.SystemBIOS)
                .HasForeignKey<SystemBIOS>(sb => sb.SystemId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            systemBIOS.HasOne(sb => sb.BIOSModel)
                .WithMany(bm => bm.SystemBIOSs)
                .HasForeignKey(sb => sb.BIOSModelId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            systemBIOS.HasKey(sb => new { sb.SystemId });

            var biosModel = builder.Entity<BIOSModel>();

            biosModel.HasOne(bm => bm.Manufacturer)
                .WithMany(m => m.BIOSManufacturer)
                .HasForeignKey(bm => bm.ManufacturerId)
                .OnDelete(DeleteBehavior.Restrict);

            var systemOSEntity = builder.Entity<SystemOS>();

            systemOSEntity.HasOne(sos => sos.System)
                .WithMany(s => s.SystemOSs)
                .HasForeignKey(sos => sos.SystemId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            systemOSEntity.HasOne(sos => sos.OperatingSystem)
                .WithMany(osm => osm.SystemOSs)
                .HasForeignKey(sos => sos.OperatingSystemId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            systemOSEntity.HasOne(sos => sos.Architecture)
                .WithMany(a => a.SystemOSs)
                .HasForeignKey(sos => sos.ArchitectureId)
                .OnDelete(DeleteBehavior.Restrict);

            systemOSEntity.HasKey(sc => new { sc.SystemId, sc.OperatingSystemId });

            var systemPingData = builder.Entity<SystemPingData>();

            systemPingData.HasOne(s => s.System)
                .WithMany(s => s.SystemPingData)
                .HasForeignKey(s => s.SystemId)
                .OnDelete(DeleteBehavior.Restrict);

            systemPingData.HasKey(s => new { s.SystemId, s.SendPingTime });

            var systemCPUData = builder.Entity<SystemCPUData>();

            systemCPUData.HasOne(s => s.System)
                .WithMany(s => s.SystemCPUData)
                .HasForeignKey(s => s.SystemId)
                .OnDelete(DeleteBehavior.Restrict);

            systemCPUData.HasKey(s => new { s.SystemId, s.CollectionDateTime });

            var systemRAMData = builder.Entity<SystemRAMData>();

            systemRAMData.HasOne(s => s.System)
                .WithMany(s => s.SystemRAMData)
                .HasForeignKey(s => s.SystemId)
                .OnDelete(DeleteBehavior.Restrict);

            systemRAMData.HasKey(s => new { s.SystemId, s.CollectionDateTime });

            var systemStorageData = builder.Entity<SystemStorageData>();

            systemStorageData.HasOne(s => s.System)
                .WithMany(s => s.SystemStorageData)
                .HasForeignKey(s => s.SystemId)
                .OnDelete(DeleteBehavior.Restrict);

            systemStorageData.HasKey(s => new { s.SystemId, s.CollectionDateTime });

            var SystemStorageData = builder.Entity<SystemStorageData>();

            SystemStorageData.HasOne(s => s.System)
                .WithMany(s => s.SystemStorageData)
                .HasForeignKey(s => s.SystemId)
                .OnDelete(DeleteBehavior.Restrict);

            SystemStorageData.HasKey(s => new { s.SystemId, s.CollectionDateTime });

            var systemRunningProcesses = builder.Entity<SystemRunningProcesses>();

            systemRunningProcesses.HasOne(s => s.System)
                .WithMany(s => s.SystemRunningProcesses)
                .HasForeignKey(s => s.SystemId)
                .OnDelete(DeleteBehavior.Restrict);

            systemRunningProcesses.HasKey(s => new { s.SystemId, s.Id, s.CollectionDateTime });

            var systemCommands = builder.Entity<SystemCommand>();

            systemCommands.HasOne(s => s.System)
                .WithMany(s => s.SystemCommands)
                .HasForeignKey(s => s.SystemId)
                .OnDelete(DeleteBehavior.Restrict);

            systemCommands.HasKey(s => new { s.SystemId, s.QueuedTime });

        }

    }
}
