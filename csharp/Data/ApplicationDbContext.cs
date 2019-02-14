using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace csharp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<SystemType> SystemTypes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<SystemModel> SystemModels { get; set; }
        public DbSet<System> Systems { get; set; }

        public DbSet<Architecture> Architectures { get; set; }
        public DbSet<ProcessorModel> ProcessorModels { get; set; }
        public DbSet<SystemCPU> SystemCPUs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Model>()
                .HasOne(m => m.Manufacturer)
                .WithMany(m => m.Models)
                .HasForeignKey(m => m.ManufacturerId);

            var SystemModel = builder.Entity<SystemModel>();

            SystemModel.HasOne(sm => sm.Model)
                .WithOne(m => m.SystemModel)
                .HasForeignKey<SystemModel>(sm => sm.Id);

            SystemModel.HasOne(sm => sm.SystemType)
                .WithMany(st => st.SystemModels)
                .HasForeignKey(sm => sm.TypeId);

            builder.Entity<System>()
                .HasOne(s => s.SystemModel)
                .WithMany(sm => sm.Systems)
                .HasForeignKey(s => s.ModelId);


            var ProcessorModel = builder.Entity<ProcessorModel>();

            ProcessorModel.HasOne(pm => pm.Architecture)
                .WithMany(a => a.ProcessorModels)
                .HasForeignKey(pm => pm.ArchitectureId);

            ProcessorModel.HasOne(pm => pm.Model)
                .WithOne(m => m.ProcessorModel)
                .HasForeignKey<ProcessorModel>(pm => pm.Id);

            var SystemCPU = builder.Entity<SystemCPU>();

            SystemCPU.HasOne(sc => sc.System)
                .WithMany(s => s.SystemCPUs)
                .HasForeignKey(sc => sc.SystemId);

            SystemCPU.HasOne(sc => sc.ProcessorModel)
                .WithMany(pm => pm.SystemCPUs)
                .HasForeignKey(sc => sc.CPUModelID);

            SystemCPU.HasKey(sc => new { sc.CPUModelID, sc.SystemId });
        }       
    }
}
