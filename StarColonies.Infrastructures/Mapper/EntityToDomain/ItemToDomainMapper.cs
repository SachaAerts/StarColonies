using StarColonies.Domains.Models.Items;
using StarColonies.Infrastructures.Data.Entities.Items;

namespace StarColonies.Infrastructures.Mapper.EntityToDomain;

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