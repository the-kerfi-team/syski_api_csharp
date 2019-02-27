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
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<SystemModel> SystemModels { get; set; }
        public DbSet<SystemModelType> SystemModelTypes { get; set; }
        public DbSet<System> Systems { get; set; }

        public DbSet<Architecture> Architectures { get; set; }
        public DbSet<ProcessorModel> ProcessorModels { get; set; }
        public DbSet<SystemCPU> SystemCPUs { get; set; }

        public DbSet<OperatingSystem> OperatingSystems { get; set; }
        public DbSet<SystemOS> SystemOSs { get; set; }

        public DbSet<MemoryModel> MemoryModels { get; set; }
        public DbSet<MemoryType> MemoryTypes { get; set; }
        public DbSet<RAMModel> RAMModels { get; set; }
        public DbSet<SystemRAM> SystemRAMs { get; set; }

        public DbSet<StorageModel> StorageModels { get; set; }
        public DbSet<SystemStorage> SystemStorages { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

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

            builder.Entity<Model>() 
                 .HasOne(m => m.Manufacturer)
                 .WithMany(m => m.Models)
                 .HasForeignKey(m => m.ManufacturerId);

            builder.Entity<SystemModel>()
                .HasOne(sm => sm.Model)
                .WithOne(m => m.SystemModel)
                .HasForeignKey<SystemModel>(sm => sm.Id);

            var SystemModelType = builder.Entity<SystemModelType>();

            SystemModelType.HasOne(smt => smt.SystemModel)
                .WithMany(sm => sm.SystemModelTypes)
                .HasForeignKey(smt => smt.SystemModelId)
                .OnDelete(DeleteBehavior.Restrict);

            SystemModelType.HasOne(smt => smt.Type)
                .WithMany(t => t.SystemModelTypes)
                .HasForeignKey(smt => smt.TypeId)
                .OnDelete(DeleteBehavior.Restrict);

            SystemModelType.HasKey(smt => new { smt.SystemModelId, smt.TypeId });

            var System = builder.Entity<System>();

            System.HasOne(s => s.SystemModel)
                .WithMany(sm => sm.Systems)
                .HasForeignKey(s => s.ModelId)
                .OnDelete(DeleteBehavior.Restrict);


            var ProcessorModel = builder.Entity<ProcessorModel>();

            ProcessorModel.HasOne(pm => pm.Architecture)
                .WithMany(a => a.ProcessorModels)
                .HasForeignKey(pm => pm.ArchitectureId)
                .OnDelete(DeleteBehavior.Restrict);

            ProcessorModel.HasOne(pm => pm.Model)
                .WithOne(m => m.ProcessorModel)
                .HasForeignKey<ProcessorModel>(pm => pm.Id)
                .OnDelete(DeleteBehavior.Restrict);

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


            var SystemOS = builder.Entity<SystemOS>();

            SystemOS.HasOne(so => so.System)
                .WithMany(s => s.SystemOSs)
                .HasForeignKey(so => so.SystemId)
                .OnDelete(DeleteBehavior.Restrict);

            SystemOS.HasOne(so => so.OperatingSystem)
                .WithMany(os => os.SystemOSs)
                .HasForeignKey(so => so.OSId)
                .OnDelete(DeleteBehavior.Restrict);

            SystemOS.HasOne(so => so.Architecture)
                .WithMany(a => a.SystemOSs)
                .HasForeignKey(so => so.ArchitectureId)
                .OnDelete(DeleteBehavior.Restrict);

            SystemOS.HasKey(so => new { so.SystemId, so.OSId });


            var MemoryModel = builder.Entity<MemoryModel>();

            MemoryModel.HasOne(mm => mm.Model)
                .WithOne(m => m.MemoryModel)
                .HasForeignKey<MemoryModel>(mm => mm.Id);

            MemoryModel.HasOne(mm => mm.MemoryType)
                .WithMany(mt => mt.MemoryModels)
                .HasForeignKey(mm => mm.MemoryTypeId);

            builder.Entity<RAMModel>()
                .HasOne(rm => rm.MemoryModel)
                .WithOne(mm => mm.RAMModel)
                .HasForeignKey<RAMModel>(rm => rm.Id);

            var SystemRAM = builder.Entity<SystemRAM>();

            SystemRAM.HasOne(sr => sr.System)
                .WithMany(s => s.SystemRAMs)
                .HasForeignKey(sr => sr.SystemId)
                .OnDelete(DeleteBehavior.Restrict);

            SystemRAM.HasOne(sr => sr.RAMModel)
                .WithMany(rm => rm.SystemRAMs)
                .HasForeignKey(sr => sr.RAMModelId)
                .OnDelete(DeleteBehavior.Restrict);

            SystemRAM.HasKey(sr => new { sr.SystemId, sr.RAMModelId });
            

            var SystemStorage = builder.Entity<SystemStorage>();

            SystemStorage.HasOne(ss => ss.System)
                .WithMany(s => s.SystemStorages)
                .HasForeignKey(ss => ss.SystemId)
                .OnDelete(DeleteBehavior.Restrict);

            SystemStorage.HasOne(ss => ss.StorageModel)
                .WithMany(sm => sm.SystemStorages)
                .HasForeignKey(ss => ss.StorageModelId)
                .OnDelete(DeleteBehavior.Restrict);

            SystemStorage.HasKey(ss => new { ss.SystemId, ss.StorageModelId });

            builder.Entity<StorageModel>()
                .HasOne(sm => sm.MemoryModel)
                .WithOne(mm => mm.StorageModel)
                .HasForeignKey<StorageModel>(sm => sm.Id);
        }
  
    }
}
