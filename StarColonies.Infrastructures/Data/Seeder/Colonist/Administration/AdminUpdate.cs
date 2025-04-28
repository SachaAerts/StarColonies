using Microsoft.AspNetCore.Identity;
using StarColonies.Domains.Models;
using StarColonies.Infrastructures.Data.Entities;

namespace StarColonies.Infrastructures.Data.Seeder.Colonist.Administration;

public static class AdminUpdate
{
    public static async Task UpdateAdminIfNeededAsync(UserManager<ColonistEntity> userManager, string username, ColonistEntity admin)
    {
        if (!NeedsUpdate(admin)) return;

        Update(admin);

        var updateResult = await userManager.UpdateAsync(admin);
        if (!updateResult.Succeeded) LogErrors(updateResult.Errors, username);
    }

    private static bool NeedsUpdate(ColonistEntity admin)
        => string.IsNullOrWhiteSpace(admin.ProfilPicture) || admin.JobModel != JobModel.Engineer;

    private static void Update(ColonistEntity admin)
    {
        if (string.IsNullOrWhiteSpace(admin.ProfilPicture)) admin.ProfilPicture = "1.png";
        if (admin.JobModel != JobModel.Engineer) admin.JobModel = JobModel.Engineer;
    }
    
    private static void LogErrors(IEnumerable<IdentityError> errors, string user)
    {
        foreach (var error in errors) Console.WriteLine($"[SEED ERROR] {user}: {error.Description}");
    }
}