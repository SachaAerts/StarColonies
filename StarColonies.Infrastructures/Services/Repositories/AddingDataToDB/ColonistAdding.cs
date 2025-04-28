using StarColonies.Domains.Models.Colony;
using StarColonies.Infrastructures.Data;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Mapper.DomainToEntity;

namespace StarColonies.Infrastructures.Services.Repositories.AddingDataToDB;

public class ColonistAdding(StarColoniesDbContext context,
    IDomainToEntityMapper<ColonistEntity, ColonistModel> colonistReverseMapper) : IAdding<ColonistModel>
{
    public async Task AddAsync(ColonistModel entity)
    {
        var colonist = colonistReverseMapper.Map(entity);
        context.Users.Add(colonist);
        await context.SaveChangesAsync();
    }
}