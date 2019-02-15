﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace csharp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<SystemModel> SystemModels { get; set; }
        public DbSet<SystemModelType> SystemModelTypes { get; set; }
        public DbSet<System> Systems { get; set; }

        /*public DbSet<Architecture> Architectures { get; set; }
        public DbSet<ProcessorModel> ProcessorModels { get; set; }
        public DbSet<SystemCPU> SystemCPUs { get; set; }

        public DbSet<OperatingSystem> OperatingSystems { get; set; }
        public DbSet<SystemOS> SystemOSs { get; set; }*/

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

/*
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

            SystemOS.HasKey(so => new { so.SystemId, so.OSId });*/
        }       
    }
}
