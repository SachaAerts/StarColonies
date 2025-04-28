using Microsoft.AspNetCore.Identity;
using StarColonies.Infrastructures.Data.Entities;

namespace StarColonies.Infrastructures.Data.Seeder.Colonist.Administration;

public class AdminSeeder : IUserSeeder
{
    private const string Email = "admin@starcolonies.com";
    private const string Username = "admin";
    private const string Password = "Password123_";
    private const string Role = "Admin";

    public async Task SeedAsync(UserManager<ColonistEntity> userManager, RoleManager<IdentityRole> roleManager)
    {
        var admin = await userManager.FindByEmailAsync(Email);

        if (admin == null) await AdminCreating.CreateAdminAsync(userManager, Username, Email, Password, Role);
            else await AdminUpdate.UpdateAdminIfNeededAsync(userManager, Username, admin);
    }    
}