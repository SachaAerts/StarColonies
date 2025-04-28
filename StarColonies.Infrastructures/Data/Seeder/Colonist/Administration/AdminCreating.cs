using Microsoft.AspNetCore.Identity;
using StarColonies.Domains.Models;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Data.Seeder.Factories;

namespace StarColonies.Infrastructures.Data.Seeder.Colonist.Administration;

public static class AdminCreating
{
    public static async Task CreateAdminAsync(UserManager<ColonistEntity> userManager, 
        string username, string email, string password, string adminRole)
    {
        var admin = ColonistFactory.Create(username, email, new DateTime(2002, 9, 28), 10, 5, 2, 100000, "1.png", JobModel.Engineer, true);
        var creationResult = await userManager.CreateAsync(admin, password);

        if (!creationResult.Succeeded)
        {
            LogErrors(creationResult.Errors, username);
            return;
        }

        var roleResult = await userManager.AddToRoleAsync(admin, adminRole);

        if (!roleResult.Succeeded) LogErrors(roleResult.Errors, username);
    }
    
    private static void LogErrors(IEnumerable<IdentityError> errors, string user)
    {
        foreach (var error in errors) Console.WriteLine($"[SEED ERROR] {user}: {error.Description}");
    }
}