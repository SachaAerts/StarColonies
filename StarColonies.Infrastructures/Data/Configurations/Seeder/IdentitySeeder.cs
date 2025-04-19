using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using StarColonies.Domains.Models;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Data.Entities.Items;

namespace StarColonies.Infrastructures.Data.Configurations.Seeder;

public static class IdentitySeeder
{
    public static async Task SeedRolesAndUsersAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ColonistEntity>>();

        await EnsureRolesExistAsync(roleManager);
        await EnsureAdminExistsAsync(userManager);
        await EnsurePlayersExistAsync(userManager);
    }

    private static async Task EnsureRolesExistAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roles = ["Admin", "Player"];
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    private static async Task EnsureAdminExistsAsync(UserManager<ColonistEntity> userManager)
    {
        const string email = "admin@starcolonies.com";
        const string username = "admin";
        const string password = "Password123_";

        var admin = await userManager.FindByEmailAsync(email);

        if (admin != null)
        {
            bool updated = false;

            if (string.IsNullOrWhiteSpace(admin.ProfilPicture))
            {
                admin.ProfilPicture = "1.png";
                updated = true;
            }

            if (admin.JobModel != JobModel.Engineer)
            {
                admin.JobModel = JobModel.Engineer;
                updated = true;
            }

            if (updated)
                await userManager.UpdateAsync(admin);

            return;
        }

        admin = new ColonistEntity
        {
            UserName = username,
            Email = email,
            NormalizedEmail = email.ToUpperInvariant(),
            NormalizedUserName = username.ToUpperInvariant(),
            DateOfBirth = new DateTime(2002, 09, 28),
            Level = 1000,
            Strength = 1003,
            Stamina = 1003,
            Musty = 100000,
            ProfilPicture = "1.png",
            JobModel = JobModel.Engineer,
            EmailConfirmed = true,
        };

        var result = await userManager.CreateAsync(admin, password);
        if (result.Succeeded)
            await userManager.AddToRoleAsync(admin, "Admin");
        else
            LogErrors(result.Errors, "admin");
    }

    private static async Task EnsurePlayersExistAsync(UserManager<ColonistEntity> userManager)
{
    var players = new[]
    {
        new { UserName = "alpha", Email = "alpha@starcolonies.com", Job = JobModel.Scientist },
        new { UserName = "beta",  Email = "beta@starcolonies.com",  Job = JobModel.Soldier   },
        new { UserName = "gamma", Email = "gamma@starcolonies.com", Job = JobModel.Soldier   },
        new { UserName = "delta", Email = "delta@starcolonies.com", Job = JobModel.Engineer  },
        new { UserName = "omega", Email = "omega@starcolonies.com", Job = JobModel.Scientist }
    };

    foreach (var p in players)
    {
        var existing = await userManager.FindByEmailAsync(p.Email);

        if (existing != null)
        {
            bool updated = false;

            if (string.IsNullOrWhiteSpace(existing.ProfilPicture))
            {
                existing.ProfilPicture = "1.png";
                updated = true;
            }

            if (existing.JobModel != p.Job)
            {
                existing.JobModel = p.Job;
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

            if (!existing.EmailConfirmed)
            {
                existing.EmailConfirmed = true;
                updated = true;
            }

            if (updated)
            {
                var updateResult = await userManager.UpdateAsync(existing);
                if (!updateResult.Succeeded)
                    LogErrors(updateResult.Errors, p.UserName);
            }

            continue;
        }

        var player = new ColonistEntity
        {
            UserName = p.UserName,
            Email = p.Email,
            NormalizedEmail = p.Email.ToUpperInvariant(),
            NormalizedUserName = p.UserName.ToUpperInvariant(),
            DateOfBirth = new DateTime(2000, 1, 1),
            JobModel = p.Job,
            Level = 1,
            Strength = 5,
            Stamina = 5,
            Musty = 100,
            ProfilPicture = "1.png",
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(player, "Player123_");
        if (result.Succeeded)
            await userManager.AddToRoleAsync(player, "Player");
        else
            LogErrors(result.Errors, p.UserName);
    }
}

    private static void LogErrors(IEnumerable<IdentityError> errors, string user)
    {
        foreach (var error in errors)
        {
            Console.WriteLine($"[SEED ERROR] {user}: {error.Description}");
        }
    }
}
