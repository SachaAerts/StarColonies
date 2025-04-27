using Microsoft.AspNetCore.Identity;
using StarColonies.Domains.Models;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Data.Seeder.Factories;

namespace StarColonies.Infrastructures.Data.Seeder.Colonist;

public class AdminSeeder : IUserSeeder
{
    public async Task SeedAsync(UserManager<ColonistEntity> userManager, RoleManager<IdentityRole> roleManager)
    {
        const string email = "admin@starcolonies.com", 
                     username = "admin", 
                     password = "Password123_";

        var admin = await userManager.FindByEmailAsync(email);
        if (admin == null)
        {
            admin = ColonistFactory.Create(username, email, username,
                new DateTime(2002, 9, 28),
                10, 5, 2, 100000,
                "1.png", JobModel.Engineer, true);

            var result = await userManager.CreateAsync(admin, password);
            if (result.Succeeded) await userManager.AddToRoleAsync(admin, "Admin");
            else LogErrors(result.Errors, username);
        }
        else
        {
            if (UpdateAdminFields(admin))
            {
                var updateResult = await userManager.UpdateAsync(admin);
                if (!updateResult.Succeeded) LogErrors(updateResult.Errors, username);
            }
        }
    }

    private static bool UpdateAdminFields(ColonistEntity admin)
    {
        bool updated = false;

        if (string.IsNullOrWhiteSpace(admin.ProfilPicture))
        {
            admin.ProfilPicture = "1.png";
            updated = true;
        }

        if (admin.JobModel == JobModel.Engineer) return updated;
        
        admin.JobModel = JobModel.Engineer;
        updated = true;

        return updated;
    }

    private static void LogErrors(IEnumerable<IdentityError> errors, string user)
    {
        foreach (var error in errors) Console.WriteLine($"[SEED ERROR] {user}: {error.Description}");
    }
}