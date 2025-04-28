using Microsoft.AspNetCore.Identity;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Data.Seeder.Factories;
using StarColonies.Infrastructures.Data.Seeder.Registers;

namespace StarColonies.Infrastructures.Data.Seeder.Colonist.Players;

public static class PlayerCreating
{
    
    public static async Task CreateNewPlayerAsync(UserManager<ColonistEntity> userManager, Player player, string role,string defaultPassword, string defaultProfilePicture)
    {
        var newPlayer = ColonistFactory.Create(player.UserName, player.Email, new DateTime(2000, 1, 1), 
            1, 2, 5, 100, defaultProfilePicture, player.Job, true);

        var creationResult = await userManager.CreateAsync(newPlayer, defaultPassword);

        if (!creationResult.Succeeded)
        {
            LogErrors(creationResult.Errors, player.UserName);
            return;
        }

        var roleResult = await userManager.AddToRoleAsync(newPlayer, role);

        if (!roleResult.Succeeded) LogErrors(roleResult.Errors, player.UserName);
    }
    
    private static void LogErrors(IEnumerable<IdentityError> errors, string user)
    {
        foreach (var error in errors) Console.WriteLine($"[SEED ERROR] {user}: {error.Description}");
    }
    
}