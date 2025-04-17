using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using StarColonies.Domains.Models;
using StarColonies.Infrastructures.Data.Entities;

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

        if (await userManager.FindByEmailAsync(email) != null) return;

        var admin = new ColonistEntity
        {
            UserName = username,
            Email = email,
            NormalizedEmail = email.ToUpperInvariant(),
            NormalizedUserName = username.ToUpperInvariant(),
            DateOfBirth = new DateTime(2002, 09, 28),
            Level = 1000,
            Strength = 1003,
            Endurance = 1003,
            Musty = 100000,
            JobModel = JobModel.Engineer,
            EmailConfirmed = true
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
            if (await userManager.FindByEmailAsync(p.Email) != null) continue;

            var player = new ColonistEntity
            {
                UserName = p.UserName,
                Email = p.Email,
                NormalizedEmail = p.Email.ToUpperInvariant(),
                NormalizedUserName = p.UserName.ToUpperInvariant(),
                DateOfBirth = new DateTime(2000, 1, 1),
                JobModel = p.Job,
                Level = 1,
                Strength = 5, Endurance = 5,
                Musty = 100,
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
