using Microsoft.AspNetCore.Identity;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Data.Seeder.Registers;

namespace StarColonies.Infrastructures.Data.Seeder.Colonist.Players;

public class PlayerSeeder : IUserSeeder
{
    private const string DefaultPassword = "Player123_";
    private const string DefaultProfilePicture = "1.png";
    private const string PlayerRole = "Player";
    
    public async Task SeedAsync(UserManager<ColonistEntity> userManager, RoleManager<IdentityRole> roleManager)
    {
        var players = PlayerRegister.GetAll();

        foreach (var player in players)
        {
            var existingPlayer = await userManager.FindByEmailAsync(player.Email);

            if (existingPlayer == null) await PlayerCreating.CreateNewPlayerAsync(userManager, player, PlayerRole, DefaultPassword, DefaultProfilePicture);
                else await PlayerUpdate.UpdatePlayerIfNeededAsync(userManager, existingPlayer, player);
        }
    }
}