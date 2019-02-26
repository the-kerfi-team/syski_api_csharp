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

            var Model = builder.Entity<Model>();

            Model.HasOne(m => m.Manufacturer)
                 .WithMany(m => m.Models)
                 .HasForeignKey(m => m.ManufacturerId);

            var SystemModelType = builder.Entity<SystemModelType>();

            SystemModelType.HasOne(smt => smt.System)
                .WithMany(sm => sm.SystemTypes)
                .HasForeignKey(smt => smt.SystemId)
                .OnDelete(DeleteBehavior.Restrict);

            SystemModelType.HasOne(smt => smt.Type)
                .WithMany(t => t.SystemModelTypes)
                .HasForeignKey(smt => smt.TypeId)
                .OnDelete(DeleteBehavior.Restrict);

            SystemModelType.HasKey(smt => new { smt.SystemId, smt.TypeId });

            var system = builder.Entity<System>();

            system.HasOne(s => s.Model)
                .WithOne(m => m.System)
                .HasForeignKey<System>(s => s.ModelId)
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

        }
  
    }
}
