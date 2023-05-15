using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PersonalCollections.Data.Static;
using PersonalCollections.Models;

namespace PersonalCollections.Data
{
	public class AppDbContext : IdentityDbContext<ApplicationUser>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Collection>()
                .HasMany(c => c.Items)
                .WithMany(i => i.Collection);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.CreatedCollections)
                .WithOne(c => c.CreatedBy)
                .HasForeignKey(c => c.CreatedByUserId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.UpdatedCollections)
                .WithOne(c => c.UpdatedBy)
                .HasForeignKey(c => c.UpdatedByUserId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.CreatedItems)
                .WithOne(c => c.CreatedBy)
                .HasForeignKey(c => c.CreatedByUserId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.UpdatedItems)
                .WithOne(c => c.UpdatedBy)
                .HasForeignKey(c => c.UpdatedByUserId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Collection> Collections { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
 
