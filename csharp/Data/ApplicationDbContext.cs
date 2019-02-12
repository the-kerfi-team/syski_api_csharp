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
        public DbSet<Model> Models { get; set; }
        public DbSet<SystemModel> SystemModels { get; set; }
        public DbSet<SystemType> SystemTypes { get; set; }
        public DbSet<ProcessorModel> ProcessorModels { get; set; }
        public DbSet<Architecture> Architectures { get; set; }
        public DbSet<System> Systems { get; set; }
        public DbSet<SystemCPU> SystemCPUs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Model>()
                .HasOne(m => m.Manufacturer)
                .WithMany(m => m.Models)
                .HasForeignKey(m => m.ManufacturerId);

            builder.Entity<SystemModel>()
                .HasOne(m => m.Model)
                .WithOne(m => m.SystemModel)
                .HasForeignKey<SystemModel>(m => m.Id);

            builder.Entity<SystemType>()
                .HasMany(t => t.SystemModels)
                .WithOne(m => m.SystemType)
                .HasForeignKey(m => m.TypeId);

            builder.Entity<ProcessorModel>()
                .HasOne(m => m.Model)
                .WithOne(m => m.ProcessorModel)
                .HasForeignKey<ProcessorModel>(m => m.Id);

            builder.Entity<Architecture>()
                .HasMany(a => a.ProcessorModels)
                .WithOne(m => m.Architecture)
                .HasForeignKey(m => m.ArchitectureId);

            builder.Entity<System>()
                .HasOne(s => s.SystemModel)
                .WithMany(m => m.Systems)
                .HasForeignKey(s => s.ModelId);

            builder.Entity<SystemCPU>()
                .HasKey(c => new { c.CPUModelId, c.SystemID });

            builder.Entity<SystemCPU>()
                .HasOne(c => c.System)
                .WithMany(s => s.SystemCPUs)
                .HasForeignKey(c => c.SystemID);

            builder.Entity<SystemCPU>()
                .HasOne(c => c.ProcessorModel)
                .WithMany(m => m.SystemCPUs)
                .HasForeignKey(c => c.CPUModelId);

        }
    }
}
