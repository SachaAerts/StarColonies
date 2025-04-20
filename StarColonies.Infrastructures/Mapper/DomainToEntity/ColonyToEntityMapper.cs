using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Repositories;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Mapper.EntityToDomain;

namespace StarColonies.Infrastructures.Mapper.DomainToEntity;

public class ColonyToEntityMapper(
    IColonistRepository repository, 
    IDomainToEntityMapper<ColonistEntity, ColonistModel> mapper) : IDomainToEntityMapper<ColonyEntity, ColonyModel>
{
    public ColonyEntity Map(ColonyModel entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            CreatedAt = entity.CreatedAt,
            LogoPath = entity.LogoPath,
            OwnerId = entity.OwnerId,
            Owner = mapper.Map(repository.GetColonistByIdAsync(entity.OwnerId).Result)
        };
}