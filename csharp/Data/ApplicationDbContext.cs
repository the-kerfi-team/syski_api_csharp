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
        public DbSet<SystemModel> SystemModel { get; set; }
        public DbSet<System> Systems { get; set; }

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
        }       
    }
}
