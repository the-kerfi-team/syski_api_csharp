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

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

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

        }

    }
}
