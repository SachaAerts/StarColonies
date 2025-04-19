using StarColonies.Domains.Models.Missions;
using StarColonies.Infrastructures.Data.Entities.Missions;
using StarColonies.Infrastructures.Mapper.EntityToDomain;

namespace StarColonies.Infrastructures.Mapper;

public class EnemyToDomainMapper : IEntityToDomainMapper<EnemyModel, EnemyEntity>
{
    public EnemyModel Map(EnemyEntity entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Type = entity.Type.Name,
        Strength = entity.Strength,
        Stamina = entity.Stamina,
        ImagePath = entity.ImagePath
    };
}