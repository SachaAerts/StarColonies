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
    IEntityToDomainMapper<ColonistModel, ColonistEntity> colonistMapper,
    IDomainToEntityMapper<ColonyEntity, ColonyModel> reverseMapper) : IColonyRepository
{
        
    public async Task<IList<ColonyModel>> GetColoniesForColonistAsync(string colonistId)
    {
        var colonies = await context.Colony
            .Include(c => c.Members)
            .ThenInclude(m => m.Colonist)
            .Include(c => c.Owner)
            .Where(c => c.Members.Any(m => m.ColonistId == colonistId))
            .ToListAsync();
     
        return colonies.Select(mapper.Map).ToList();
    }
    
    public async Task<IList<ColonistModel>> GetColonistsForColonyAsync(int colonyId)
    {
        var colonists = await context.ColonyMember
            .Where(cm => cm.ColonyId == colonyId)
            .Include(cm => cm.Colonist)
            .Select(cm => cm.Colonist)
            .ToListAsync();

        return colonists.Select(colonistMapper.Map).ToList();
    }
    
    public async Task AddColonyAsync(ColonyModel model)
    {
        var entity = reverseMapper.Map(model);

        var memberIds = model.Colonists.Select(c => Guid.Parse(c.Id)).ToList();
        entity.Members = await BuildColonyMembersAsync(memberIds, entity);

        context.Colony.Add(entity);
        await context.SaveChangesAsync();
    }
    
    public async Task<IList<ColonyModel>> GetTop10ColoniesAsync()
    {
        var colonies = await context.Colony
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
    
    private Task<List<ColonyMemberEntity>> BuildColonyMembersAsync(List<Guid> colonistIds, ColonyEntity colony)
    {
        return Task.FromResult(colonistIds.Select(id => new ColonyMemberEntity
        {
            ColonistId = id.ToString(),
            Colony = colony             
        }).ToList());
    }
}
