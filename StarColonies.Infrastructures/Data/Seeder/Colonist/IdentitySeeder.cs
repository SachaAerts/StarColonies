using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using StarColonies.Infrastructures.Data.Entities;

namespace StarColonies.Infrastructures.Data.Seeder.Colonist;

public static class IdentitySeeder
{
    public static async Task SeedRolesAndUsersAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ColonistEntity>>();

        await EnsureRolesExistAsync(roleManager);

        var seeders = new List<IUserSeeder> { new AdminSeeder(), new PlayerSeeder() };

        foreach (var seeder in seeders) await seeder.SeedAsync(userManager, roleManager);
    }

    private static async Task EnsureRolesExistAsync(RoleManager<IdentityRole> roleManager)
    {
        foreach (var role in new [] {"Admin", "Player"})
            if (!await roleManager.RoleExistsAsync(role)) 
                await roleManager.CreateAsync(new IdentityRole(role));
    }
}