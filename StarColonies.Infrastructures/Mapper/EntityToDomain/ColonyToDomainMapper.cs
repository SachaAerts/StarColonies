using StarColonies.Domains.Models.Colony;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Mapper.EntityToDomain;

namespace StarColonies.Infrastructures.Mapper;

public class ColonyToDomainMapper : IEntityToDomainMapper<ColonyModel, ColonyEntity>
{
    public ColonyModel Map(ColonyEntity entity) 
        => new()
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatedAt = entity.CreatedAt,
                OwnerId = entity.OwnerId
            };
}