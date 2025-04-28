using Microsoft.EntityFrameworkCore;
using StarColonies.Domains.Models.Colony;
using StarColonies.Infrastructures.Data;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Mapper.EntityToDomain;

namespace StarColonies.Infrastructures.Services.Repositories.GettingDataToDB;

public class ColonistGetting(
    StarColoniesDbContext context,
    IEntityToDomainMapper<ColonistModel, ColonistEntity> colonistMapper) : IGetting<ColonistModel, string>
{
    public async Task<IList<ColonistModel>> GetIListDataAsync()
    {
        var colonists = await context.Users.ToListAsync();
        return colonists.Select(colonistMapper.Map).ToList();
    }
    
    public async Task<ColonistModel> GetEntityByIdAsync(string id)
    {
        var entity = await context.Users.FindAsync(id);
        return entity != null ? colonistMapper.Map(entity) : null!;
    }
    
}