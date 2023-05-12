using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
            modelBuilder.Entity<Tag_Item>()
                .HasKey(ti => new
                {
                    ti.ItemId,
                    ti.TagId
                });

            modelBuilder.Entity<Tag_Item>()
                .HasOne(t => t.Tag)
                .WithMany(ti => ti.Tags_Items)
                .HasForeignKey(t => t.TagId);

            modelBuilder.Entity<Tag_Item>()
                .HasOne(t => t.Item)
                .WithMany(ti => ti.Tags_Items)
                .HasForeignKey(t => t.ItemId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Collection> Collections { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Tag_Item> Tags_Items { get; set; }
    }
}
 
