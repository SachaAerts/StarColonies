using StarColonies.Domains.Models.Items;
using StarColonies.Infrastructures.Data.Entities.Items;
using StarColonies.Infrastructures.Mapper.EntityToDomain;

namespace StarColonies.Infrastructures.Mapper;

public class ItemToDomainMapper(IEntityToDomainMapper<EffectModel, EffectEntity> effectMapper) : IEntityToDomainMapper<ItemModel, ItemEntity>
{
    public ItemModel Map(ItemEntity entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            CoinsValue = entity.CoinsValue,
            ImagePath = entity.ImagePath,
            Effect = effectMapper.Map(entity.Effect)
        };
}