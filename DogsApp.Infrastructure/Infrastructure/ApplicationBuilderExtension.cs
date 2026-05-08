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

        await RoleSeeder(services);
        await SeedAdministrator(services);

        var data = services.GetRequiredService<ApplicationDbContext>();
        SeedBrands(data);

        return app;
    }

    private static async Task RoleSeeder(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roleNames = { "Administrator", "Client" };

        IdentityResult roleResult;

        foreach (var role in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(role);

            if (!roleExist)
            {
                roleResult = await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    private static async Task SeedAdministrator(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        if (await userManager.FindByNameAsync("admin") == null)
        {
            ApplicationUser user = new ApplicationUser();
            user.FirstName = "admin";
            user.LastName = "admin";
            user.PhoneNumber = "0888888888";
            user.UserName = "admin";
            user.Email = "admin@admin.com";

            var result = await userManager.CreateAsync(user, "Admin123456");
            
            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Administrator").Wait();
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