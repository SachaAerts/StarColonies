using StarColonies.Domains.Repositories;
using StarColonies.Infrastructures.Data;

namespace StarColonies.Infrastructures.Repositories;

public class ColonistFinanceRepository(StarColoniesDbContext context) : IColonistFinanceRepository
{
    public async Task DebitColonistAsync(string id, int amount)
    {
        var entity = await context.Users.FindAsync(id);
        if (entity == null) return;

        entity.Musty -= amount;
        if (entity.Musty < 0) entity.Musty = 0;
        
        await context.SaveChangesAsync();
    }

    public async Task AddMustyColonistAsync(string id, int amount)
    {
        var entity = await context.Users.FindAsync(id);
        if (entity == null) return;

        entity.Musty += amount;
        if (entity.Musty < 0) entity.Musty = 0;

        await context.SaveChangesAsync();
    }
}