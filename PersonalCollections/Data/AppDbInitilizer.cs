using System;
using System.ComponentModel.Design;
using Microsoft.AspNetCore.Identity;
using PersonalCollections.Data.Static;
using PersonalCollections.Models;

namespace PersonalCollections.Data
{
  public class AppDbInitilizer
  {
    public static void Seed(IApplicationBuilder applicationBuilder)
    {
      using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
      {
        var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

        context.Database.EnsureCreated();

        //collections
        if (!context.Collections.Any())
        {
          context.Collections.AddRange(new List<Collection>()
            {
                new Collection()
                {
                    Title = "Books",
                    Description = "Collection of books",
                    Subject = "book"
                },
                new Collection()
                {
                    Title = "Cars",
                    Description = "Collection of cars",
                    Subject = "mersedes"
                },
                new Collection()
                {
                    Title = "Films",
                    Description = "Collection of films",
                    Subject = "films"
                }
            });
          context.SaveChanges();
        }
        //comments
        if (!context.Comments.Any())
        {
          context.Comments.AddRange(new List<Comment>()
            {
                new Comment()
                {
                    Text = "Comment 1",
                    CreatedAt = DateTime.UtcNow,
                },
                new Comment()
                {
                    Text = "Comment 2",
                    CreatedAt = DateTime.UtcNow,
                },
                new Comment()
                {
                    Text = "Comment 3",
                    CreatedAt = DateTime.UtcNow,
                },
            });
          context.SaveChanges();
        }
        //items
        if (!context.Items.Any())
        {
          context.Items.AddRange(new List<Item>()
            {
                new Item()
                {
                    Title = "War and peace",
                    Description = "roman",
                    CreatedAt = DateTime.UtcNow,
                    Likes = 5,
                    CollectionId = 1,
                    CommentId = 1
                },
                new Item()
                {
                    Title = "Mersedes",
                    Description = "New car",
                    CreatedAt = DateTime.UtcNow,
                    Likes = 8,
                    CollectionId = 2,
                    CommentId = 2
                },
                new Item()
                {
                    Title = "Beef",
                    Description = "serial",
                    CreatedAt = DateTime.UtcNow,
                    Likes = 30,
                    CollectionId = 3,
                    CommentId = 3
                }
            });
          context.SaveChanges();
        }
        //tags
        if (!context.Tags.Any())
        {
          context.Tags.AddRange(new List<Tag>()
            {
                new Tag()
                {
                    Name = "book"
                },
                new Tag()
                {
                    Name = "car"
                },
                new Tag()
                {
                    Name = "film"
                }
            });
          context.SaveChanges();

        }
        //tag & items
        if (!context.Tags_Items.Any())
        {
          context.Tags_Items.AddRange(new List<Tag_Item>()
            {
                new Tag_Item()
                {
                    ItemId = 2,
                    TagId = 2
                },
                new Tag_Item()
                {
                    ItemId = 3,
                    TagId = 3
                },
                new Tag_Item()
                {
                    ItemId = 4,
                    TagId = 1
                }
            });
          context.SaveChanges();
        }
      }
    }

    public static async Task SeedRolesAsync(IApplicationBuilder applicationBuilder)
    {
      using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
      {

        //Roles
        var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
          await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        if (!await roleManager.RoleExistsAsync(UserRoles.User))
          await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
        if (!await roleManager.RoleExistsAsync(UserRoles.Guest))
          await roleManager.CreateAsync(new IdentityRole(UserRoles.Guest));
      }
    }
  }
}

