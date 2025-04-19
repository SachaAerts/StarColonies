using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Models.Missions;
using StarColonies.Infrastructures.Data.Entities.Items;
using StarColonies.Infrastructures.Data.Entities.Missions;
using StarColonies.Infrastructures.Mapper.EntityToDomain;

namespace StarColonies.Infrastructures.Mapper;

public class MissionToDomainMapper(
    IEntityToDomainMapper<EnemyModel, EnemyEntity> enemyMapper, 
    IEntityToDomainMapper<ItemModel, ItemEntity> itemMapper) : IEntityToDomainMapper<MissionModel, MissionEntity>
{
    public MissionModel Map(MissionEntity entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Difficulty = entity.Difficulty,
            CoinsReward = entity.CoinsReward,
            Enemies = entity.Enemies.Select(enemyMapper.Map).ToList(),
            Items = entity.Rewards
                .Select(r => itemMapper.Map(r.Item))
                .DistinctBy(i => i.Id)
                .ToList()
        };
}