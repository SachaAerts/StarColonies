using StarColonies.Domains.Models.Colony;
using StarColonies.Infrastructures.Data;

namespace StarColonies.Infrastructures.Services.Repositories.DeletingDataToDB;

public class ColonistDeleting(StarColoniesDbContext context) : IDeleting<ColonistModel>
{
    public async Task DeleteEntityAsync(string id, ColonistModel colon)
    {
        var entity = await context.Users.FindAsync(id);
        if (entity == null) return;

        context.Users.Remove(entity);
        await context.SaveChangesAsync();
    }
}