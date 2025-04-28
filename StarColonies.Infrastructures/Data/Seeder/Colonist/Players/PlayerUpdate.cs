using Microsoft.AspNetCore.Identity;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Data.Seeder.Registers;

namespace StarColonies.Infrastructures.Data.Seeder.Colonist.Players;

public static class PlayerUpdate
{
    public static async Task UpdatePlayerIfNeededAsync(UserManager<ColonistEntity> userManager, ColonistEntity existingPlayer, Player player)
    {
        if (!NeedsUpdate(existingPlayer, player)) return;

        Update(existingPlayer, player);

        var updateResult = await userManager.UpdateAsync(existingPlayer);

        if (!updateResult.Succeeded) LogErrors(updateResult.Errors, player.UserName);
    }
    
    private static bool NeedsUpdate(ColonistEntity existing, Player player)
        => string.IsNullOrWhiteSpace(existing.ProfilPicture)
               || existing.JobModel != player.Job
               || existing.Level != 1
               || existing.Strength != 5
               || existing.Stamina != 5
               || existing.Musty != 100
               || !existing.EmailConfirmed;
    
    private static void Update(ColonistEntity existing, Player player)
    {
        if (string.IsNullOrWhiteSpace(existing.ProfilPicture)) existing.ProfilPicture = "1.png";
        if (existing.JobModel != player.Job) existing.JobModel = player.Job;

        existing.Level = 1;
        existing.Strength = 5;
        existing.Stamina = 5;
        existing.Musty = 100;
        existing.EmailConfirmed = true;
    }
    
    private static void LogErrors(IEnumerable<IdentityError> errors, string user)
    {
        foreach (var error in errors) Console.WriteLine($"[SEED ERROR] {user}: {error.Description}");
    }
}