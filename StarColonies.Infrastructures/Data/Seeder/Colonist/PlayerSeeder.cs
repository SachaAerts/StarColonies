using Microsoft.AspNetCore.Identity;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Data.Seeder.Factories;

namespace StarColonies.Infrastructures.Data.Seeder.Colonist;

public class PlayerSeeder : IUserSeeder
{
    public async Task SeedAsync(UserManager<ColonistEntity> userManager, RoleManager<IdentityRole> roleManager)
    {
        var players = Players.GetAll();

        foreach (var player in players)
        {
            var existing = await userManager.FindByEmailAsync(player.Email);

            if (existing == null)
            {
                var newPlayer = ColonistFactory.Create(player.UserName, player.Email, player.UserName,
                    new DateTime(2000, 1, 1),
                    1, 2, 5, 100,
                    "1.png", player.Job, true);

                var result = await userManager.CreateAsync(newPlayer, "Player123_");
                if (result.Succeeded) await userManager.AddToRoleAsync(newPlayer, "Player");
                else LogErrors(result.Errors, player.UserName);

                continue;
            }

            if (!UpdatePlayerFields(existing, player)) continue;

            var updateResult = await userManager.UpdateAsync(existing);
            if (!updateResult.Succeeded) LogErrors(updateResult.Errors, player.UserName);
        }
    }

    private static bool UpdatePlayerFields(ColonistEntity existing, Player player)
    {
        bool updated = false;

        if (string.IsNullOrWhiteSpace(existing.ProfilPicture))
        {
            existing.ProfilPicture = "1.png";
            updated = true;
        }

        if (existing.JobModel != player.Job)
        {
            existing.JobModel = player.Job;
            updated = true;
        }

        if (existing.Level != 1 || existing.Strength != 5 || existing.Stamina != 5 || existing.Musty != 100)
        {
            existing.Level = 1;
            existing.Strength = 5;
            existing.Stamina = 5;
            existing.Musty = 100;
            updated = true;
        }

        if (existing.EmailConfirmed) return updated;
        existing.EmailConfirmed = true;
        updated = true;

        return updated;
    }

    private static void LogErrors(IEnumerable<IdentityError> errors, string user)
    {
        foreach (var error in errors) Console.WriteLine($"[SEED ERROR] {user}: {error.Description}");
    }
}