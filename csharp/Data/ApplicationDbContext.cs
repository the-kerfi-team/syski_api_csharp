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

        DbSet<Token> TheUserTokens { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Token>()
                   .HasOne(t => t.User)
                   .WithMany(u => u.Tokens)
                   .HasForeignKey(f => f.UserId)
                   .IsRequired();

        }

    }
}
