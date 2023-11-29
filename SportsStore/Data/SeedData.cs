using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

namespace SportsStore.Data;

public static class SeedData
{
    public static void EnsurePopulated(IApplicationBuilder app)
    {
        AppDbContext context = app.ApplicationServices
            .CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();

        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
        if (!context.Categories.Any())
        {
            context.Categories.AddRange(
                new Category { CategoryName = "Watersports" },
                new Category { CategoryName = "Soccer" },
                new Category
                {
                    CategoryName = "Chess"
                });
        }
        if (!context.Products.Any())
        {
            context.Products.AddRange(
                new Product
                {
                    Name = "Kayak",
                    Description = "A boat for one person",
                    CategoryID = 1,
                    Price = 275
                },
                new Product
                {
                    Name = "Lifejacket",
                    Description = "Protective and fashionable",
                    CategoryID = 1,
                    Price = 48.95m
                },
                new Product
                {
                    Name = "Soccer Ball",
                    Description = "FIFA-approved size and weight",
                    CategoryID = 2,
                    Price = 19.50m
                },
                new Product
                {
                    Name = "Corner Flags",
                    Description = "Give your playing field a professional touch",
                    CategoryID = 2,
                    Price = 34.95m
                },
                new Product
                {
                    Name = "Stadium",
                    Description = "Flat-packed 35,000-seat stadium",
                    CategoryID = 2,
                    Price = 79500
                },
                new Product
                {
                    Name = "Thinking Cap",
                    Description = "Improve brain efficiency by 75%",
                    CategoryID = 3,
                    Price = 16
                },
                new Product
                {
                    Name = "Unsteady Chair",
                    Description = "Secretly give your opponent a disadvantage",
                    CategoryID = 3,
                    Price = 29.95m
                },
                new Product
                {
                    Name = "Human Chess Board",
                    Description = "A fun game for the family",
                    CategoryID = 3,
                    Price = 75
                },
                new Product
                {
                    Name = "Bling-Bling King",
                    Description = "Gold-plated, diamond-studded King",
                    CategoryID = 3,
                    Price = 1200
                }
            );
            context.SaveChanges();
        }

        CreateAdminUser(app);
    }

    //admin user seed
    public static async void CreateAdminUser(IApplicationBuilder app)
    {
        const string adminUser = "Admin";
        const string adminPassword = "Admin$123";
        const string adminEmail = "Admin@mail.com";
        const string adminRole = "Administrator";

        UserManager<IdentityUser> userManager = app.ApplicationServices
                                                   .CreateScope()
                                                   .ServiceProvider
                                                   .GetRequiredService<UserManager<IdentityUser>>();

        RoleManager<IdentityRole> roleManager = app.ApplicationServices
                                                   .CreateScope()
                                                   .ServiceProvider
                                                   .GetRequiredService<RoleManager<IdentityRole>>();

        if(await userManager.FindByIdAsync( adminUser ) == null)
        {
            if(await roleManager.FindByNameAsync( adminRole ) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            IdentityUser user = new()
            {
                UserName = adminUser,
                Email = adminEmail,
            };

            IdentityResult result = await userManager.CreateAsync(user, adminPassword);

            if(result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, adminRole);
            }
        }
    }
}
