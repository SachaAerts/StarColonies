using StarColonies.Domains.Models.Colony;
using StarColonies.Infrastructures.Data;

namespace StarColonies.Infrastructures.Services.Repositories.UpdateDataToDB;

public class ColonistUpdate(
    StarColoniesDbContext context
    ) : IUpdate<ColonistModel>
{
    public async Task UpdateAsync(ColonistModel entity)
    {
        var colonist = await context.Users.FindAsync(entity.Id);
        if (colonist == null) return;
        
        colonist.UserName = entity.Name;
        colonist.Email = entity.Email;
        colonist.NormalizedEmail = entity.Email?.ToUpperInvariant();
        colonist.NormalizedUserName = entity.Name?.ToUpperInvariant();
        colonist.DateOfBirth = entity.DateOfBirth;
        colonist.JobModel = entity.Job;
        colonist.Level = entity.Level;
        colonist.Strength = entity.Strength;
        colonist.Stamina = entity.Stamina;
        colonist.Musty = entity.Musty;
        colonist.ProfilPicture = entity.ProfilPicture;

        await context.SaveChangesAsync();
    }
}