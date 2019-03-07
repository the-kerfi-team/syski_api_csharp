using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace csharp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Token> Tokens { get; set; }
        public DbSet<ApplicationUserSystems> ApplicationUserSystems { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<SystemType> SystemTypes { get; set; }
        public DbSet<System> Systems { get; set; }
        public DbSet<Architecture> Architectures { get; set; }
        public DbSet<CPUModel> CPUModels { get; set; }
        public DbSet<SystemCPU> SystemCPUs { get; set; }
        public DbSet<OperatingSystem> OperatingSystems { get; set; }
        public DbSet<SystemOS> SystemOSs { get; set; }
        public DbSet<RAMModel> RAMModels { get; set; }
        public DbSet<SystemRAM> SystemRAMs { get; set; }
        public DbSet<GPUModel> GPUModels { get; set; }
        public DbSet<SystemGPU> SystemGPUs { get; set; }
        public DbSet<StorageModel> StorageModels { get; set; }
        public DbSet<SystemStorage> SystemStorages { get; set; }
        public DbSet<StorageType> StorageTypes { get; set; }
        public DbSet<InterfaceType> InterfaceTypes { get; set; }
        public DbSet<SystemMotherboard> SystemMotherboards { get; set; }
        public DbSet<MotherboardModel> MotherboardModels { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Token Authentication
            var Token = builder.Entity<Token>();

            Token.HasOne(t => t.User)
                .WithMany(u => u.Tokens)
                .HasForeignKey(f => f.UserId)
                .IsRequired();

            Token.HasOne(t => t.NextToken)
                .WithOne(t => t.PreviousToken)
                .HasForeignKey<Token>(t => t.NextTokenId);

            Token.HasOne(t => t.PreviousToken)
                .WithOne(t => t.NextToken)
                .HasForeignKey<Token>(t => t.PreviousTokenId);

            // Application User < - - - > System (Link Table)
            var ApplicationUserSystems = builder.Entity<ApplicationUserSystems>();

            ApplicationUserSystems.HasOne(sc => sc.System)
                .WithMany(s => s.Users)
                .HasForeignKey(sc => sc.SystemId)
                .OnDelete(DeleteBehavior.Restrict);

            ApplicationUserSystems.HasOne(u => u.User)
                .WithMany(u => u.Systems)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            ApplicationUserSystems.HasKey(sc => new { sc.UserId, sc.SystemId });

            // System
            var System = builder.Entity<System>();

            System.HasOne(s => s.Model)
                .WithMany(m => m.Systems)
                .HasForeignKey(s => s.ModelId)
                .OnDelete(DeleteBehavior.Restrict);

            // SystemCPU
            var SystemCPU = builder.Entity<SystemCPU>();

            SystemCPU.HasOne(sc => sc.System)
                .WithMany(s => s.SystemCPUs)
                .HasForeignKey(sc => sc.SystemId)
                .OnDelete(DeleteBehavior.Restrict);

            SystemCPU.HasOne(sc => sc.ProcessorModel)
                .WithMany(pm => pm.SystemCPUs)
                .HasForeignKey(sc => sc.CPUModelID)
                .OnDelete(DeleteBehavior.Restrict);

            SystemCPU.HasKey(sc => new { sc.CPUModelID, sc.SystemId });

            // SystemMotherboard
            var SystemMotherboard = builder.Entity<SystemMotherboard>();

            SystemMotherboard.HasOne(sm => sm.System)
                .WithOne(sm => sm.SystemMotherboard)
                .HasForeignKey<SystemMotherboard>(sm => sm.SystemId)
                .OnDelete(DeleteBehavior.Restrict);

            SystemMotherboard.HasOne(sm => sm.MotherboardModel)
                .WithMany(sm => sm.SystemMotherboards)
                .HasForeignKey(sm => sm.MotherboardModelId)
                .OnDelete(DeleteBehavior.Restrict);

            SystemMotherboard.HasKey(sc => new { sc.SystemId });

            // System OS
            var SystemOS = builder.Entity<SystemOS>();

            SystemOS.HasOne(so => so.System)
                .WithMany(s => s.SystemOSs)
                .HasForeignKey(so => so.SystemId)
                .OnDelete(DeleteBehavior.Restrict);

            SystemOS.HasOne(so => so.OperatingSystem)
                .WithMany(os => os.SystemOSs)
                .HasForeignKey(so => so.OperatingSystemId)
                .OnDelete(DeleteBehavior.Restrict);

            SystemOS.HasOne(so => so.Architecture)
                .WithMany(a => a.SystemOSs)
                .HasForeignKey(so => so.ArchitectureId)
                .OnDelete(DeleteBehavior.Restrict);

            SystemOS.HasKey(so => new { so.SystemId, so.OperatingSystemId });

            // SystemRAM
            var SystemRAM = builder.Entity<SystemRAM>();

            SystemRAM.HasOne(sr => sr.System)
                .WithMany(s => s.SystemRAMs)
                .HasForeignKey(sr => sr.SystemId)
                .OnDelete(DeleteBehavior.Restrict);

            SystemRAM.HasOne(sr => sr.RAMModel)
                .WithMany(rm => rm.SystemRAMs)
                .HasForeignKey(sr => sr.RAMModelId)
                .OnDelete(DeleteBehavior.Restrict);

            SystemRAM.HasKey(sr => new { sr.SystemId, sr.RAMModelId, sr.DimmSlot });

            // SystemGPU
            var SystemGPU = builder.Entity<SystemGPU>();

            SystemGPU.HasOne(sg => sg.System)
                .WithMany(s => s.SystemGPUs)
                .HasForeignKey(sg => sg.SystemId)
                .OnDelete(DeleteBehavior.Restrict);

            SystemGPU.HasOne(sg => sg.GPUModel)
                .WithMany(gm => gm.SystemGPUs)
                .HasForeignKey(sg => sg.GPUModelId)
                .OnDelete(DeleteBehavior.Restrict);

            SystemGPU.HasKey(sg => new { sg.SystemId, sg.GPUModelId });

            // SystemStorage
            var SystemStorage = builder.Entity<SystemStorage>();

            SystemStorage.HasOne(ss => ss.System)
                .WithMany(s => s.SystemStorages)
                .HasForeignKey(ss => ss.SystemId)
                .OnDelete(DeleteBehavior.Restrict);

            SystemStorage.HasOne(ss => ss.StorageModel)
                .WithMany(sm => sm.SystemStorages)
                .HasForeignKey(ss => ss.StorageModelId)
                .OnDelete(DeleteBehavior.Restrict);

            SystemStorage.HasOne(ss => ss.Interface)
                .WithMany(it => it.SystemStorages)
                .HasForeignKey(ss => ss.InterfaceId)
                .OnDelete(DeleteBehavior.Restrict);

            SystemStorage.HasOne(ss => ss.Type)
                .WithMany(s => s.SystemStorages)
                .HasForeignKey(ss => ss.TypeId)
                .OnDelete(DeleteBehavior.Restrict);

            SystemStorage.HasKey(ss => new { ss.SystemId, ss.StorageModelId });

            // SystemType
            var SystemType = builder.Entity<SystemType>();

            SystemType.HasOne(smt => smt.System)
                .WithMany(sm => sm.SystemTypes)
                .HasForeignKey(smt => smt.SystemId)
                .OnDelete(DeleteBehavior.Restrict);

            SystemType.HasOne(smt => smt.Type)
                .WithMany(t => t.SystemModelTypes)
                .HasForeignKey(smt => smt.TypeId)
                .OnDelete(DeleteBehavior.Restrict);

            SystemType.HasKey(smt => new { smt.SystemId, smt.TypeId });

            // Model
            var Model = builder.Entity<Model>();

            Model.HasOne(m => m.Manufacturer)
                 .WithMany(m => m.Models)
                 .HasForeignKey(m => m.ManufacturerId);

            // CPUModel
            var CPUModel = builder.Entity<CPUModel>();

            CPUModel.HasOne(pm => pm.Architecture)
                .WithMany(a => a.CPUModels)
                .HasForeignKey(pm => pm.ArchitectureId)
                .OnDelete(DeleteBehavior.Restrict);

            CPUModel.HasOne(pm => pm.Model)
                .WithOne(m => m.ProcessorModel)
                .HasForeignKey<CPUModel>(pm => pm.ModelId)
                .OnDelete(DeleteBehavior.Restrict);

            // RAMModel
            var RAMModel = builder.Entity<RAMModel>();

            RAMModel.HasOne(rm => rm.Model)
                .WithOne(m => m.RAMModel)
                .HasForeignKey<RAMModel>(rm => rm.ModelId)
                .OnDelete(DeleteBehavior.Restrict);

            // GPUModel
            var GPUModel = builder.Entity<GPUModel>();

            GPUModel.HasOne(gm => gm.Model)
                .WithOne(m => m.GPUModel)
                .HasForeignKey<GPUModel>(pm => pm.ModelId)
                .OnDelete(DeleteBehavior.Restrict);

            // StorageModel
            var StorageModel = builder.Entity<StorageModel>();

            StorageModel.HasOne(gm => gm.Model)
                .WithOne(m => m.StorageModel)
                .HasForeignKey<StorageModel>(pm => pm.ModelId)
                .OnDelete(DeleteBehavior.Restrict);

            StorageModel.HasMany(sm => sm.SystemStorages)
                .WithOne(mm => mm.StorageModel)
                .HasForeignKey(sm => sm.StorageModelId)
                .OnDelete(DeleteBehavior.Restrict);

            // MotherboardModel
            var MotherboardModel = builder.Entity<MotherboardModel>();

            MotherboardModel.HasOne(mm => mm.Model)
                .WithOne(m => m.MotherboardModel)
                .HasForeignKey<MotherboardModel>(mm => mm.ModelId)
                .OnDelete(DeleteBehavior.Restrict);

            MotherboardModel.HasMany(mm => mm.SystemMotherboards)
                .WithOne(s => s.MotherboardModel)
                .HasForeignKey(s => s.MotherboardModelId)
                .OnDelete(DeleteBehavior.Restrict);
        }
  
    }
}
