using Microsoft.EntityFrameworkCore;
using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Repositories;
using StarColonies.Infrastructures.Data;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Mapper.DomainToEntity;
using StarColonies.Infrastructures.Mapper.EntityToDomain;

namespace StarColonies.Infrastructures.Repositories;

public class ColonyRepository(
    StarColoniesDbContext context, 
    IEntityToDomainMapper<ColonyModel, ColonyEntity> mapper, 
    IDomainToEntityMapper<ColonyEntity, ColonyModel> reverseMapper) : IColonyRepository
{
        
    public async Task<IList<ColonyModel>> GetColoniesForColonistAsync(string colonistId)
    {
        var colonies = await context.Colonies
            .Include(c => c.Owner)
            .Where(c => c.Members.Any(m => m.ColonistId == colonistId))
            .ToListAsync();

        return colonies.Select(mapper.Map).ToList();
    }
    
    public void AddColony(ColonyModel colony) 
        => context.Colonies.Add(reverseMapper.Map(colony));
    
    public async Task<IList<ColonyModel>> GetTop10ColoniesAsync()
    {
        var colonies = await context.Colonies
            .Include(c => c.Members)
            .ThenInclude(m => m.Colonist)
            .Include(c => c.Owner)
            .ToListAsync();

        var colonyModels = colonies
            .Select(mapper.Map)
            .Where(c => c.Colonists.Any())
            .OrderByDescending(c => c.Strength)
            .Take(10)
            .ToList();

        return colonyModels;
    }
}