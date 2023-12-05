using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Data;

public static class SeedDataIdentity
{
    private const string adminUser = "Admin";
    private const string adminPassword = "Admin$123";
    private const string adminEmail = "Admin@mail.com";
    private const string adminRole = "Administrator";

    public static async void EnsurePopulated(IApplicationBuilder app)
    {
        AppIdentityDbContext context = app.ApplicationServices
                                          .CreateScope()
                                          .ServiceProvider
                                          .GetRequiredService<AppIdentityDbContext>();

        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }

        UserManager<IdentityUser> userManager = app.ApplicationServices
                                                   .CreateScope()
                                                   .ServiceProvider
                                                   .GetRequiredService<UserManager<IdentityUser>>();

        RoleManager<IdentityRole> roleManager = app.ApplicationServices
                                                   .CreateScope()
                                                   .ServiceProvider
                                                   .GetRequiredService<RoleManager<IdentityRole>>();

        if (await userManager.FindByIdAsync(adminUser) == null)
        {
            if (await roleManager.FindByNameAsync(adminRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            IdentityUser user = new()
            {
                UserName = adminUser,
                Email = adminEmail,
            };

            IdentityResult result = await userManager.CreateAsync(user, adminPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, adminRole);
            }
        }


    }
}
