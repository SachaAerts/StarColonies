using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Models.Missions;
using StarColonies.Infrastructures.Data.Entities.Items;
using StarColonies.Infrastructures.Data.Entities.Missions;

namespace StarColonies.Infrastructures.Mapper.EntityToDomain;

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
            Visible = entity.Visible,
            Enemies = entity.Enemies.Select(enemyMapper.Map).ToList(),
            Items = entity.Rewards
                .Select(r => new RewardItemModel
                    {   
                        Item = itemMapper.Map(r.Item),
                        Quantity = r.Quantity
                    }).ToList()
        };
}