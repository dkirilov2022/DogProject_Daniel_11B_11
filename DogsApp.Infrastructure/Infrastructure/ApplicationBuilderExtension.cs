using DogsProject_Daniel_11_11.Data;
using DogsProject_Daniel_11_11.Data.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DogsApp.Infrastructure.Infrastructure;

public static class ApplicationBuilderExtension
{
    public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();

        var services = serviceScope.ServiceProvider;

        var data = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        SeedBrands(data);
        await SeedRoles(roleManager);
        await SeedAdmin(userManager);

        return app;
    }

    private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
    {
        if (!await roleManager.RoleExistsAsync("Administrator"))
        {
            await roleManager.CreateAsync(new IdentityRole("Administrator"));
        }
    }

    private static async Task SeedAdmin(UserManager<ApplicationUser> userManager)
    {
        if (await userManager.FindByNameAsync("admin") == null)
        {
            var user = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@dogs.com",
                FirstName = "Admin",
                LastName = "User",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, "Admin123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Administrator");
            }
        }
    }

    private static void SeedBrands(ApplicationDbContext data)
    {
        if (data.Breeds.Any())
        {
            return;
        }
        
        data.Breeds.AddRange(new[]
        {
            new Breed {Name = "Husky"},
            new Breed {Name = "Pinscher"},
            new Breed {Name = "Cocer spaniol"},
            new Breed {Name = "Dachshund"},
            new Breed {Name = "Doberman"},
        });
        
        data.SaveChanges();
    }
}