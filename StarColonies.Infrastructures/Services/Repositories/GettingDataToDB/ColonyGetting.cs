using Microsoft.EntityFrameworkCore;
using StarColonies.Domains.Models.Colony;
using StarColonies.Infrastructures.Data;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Mapper.EntityToDomain;

namespace StarColonies.Infrastructures.Services.Repositories.GettingDataToDB;

public class ColonyGetting(
    StarColoniesDbContext context,
    IEntityToDomainMapper<ColonyModel?, ColonyEntity> mapper) : IGetting<ColonyModel, int>
{
    public async Task<IList<ColonyModel>> GetIListDataAsync()
    {
        var colonies = await context.Colony
            .Include(c => c.Members)
            .ThenInclude(m => m.Colonist)
            .Include(c => c.Owner)
            .ToListAsync();

        return colonies.Select(mapper.Map).ToList()!;
    }

    public Task<IList<ColonyModel>> GetIListDataAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ColonyModel> GetEntityByIdAsync(int id)
    {
        var colony = await context.Colony
            .Include(c => c.Members)
            .ThenInclude(m => m.Colonist)
            .Include(c => c.Owner)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (colony == null)
            throw new InvalidOperationException($"Colony with ID {id} not found.");

        return mapper.Map(colony)!;
    }
}