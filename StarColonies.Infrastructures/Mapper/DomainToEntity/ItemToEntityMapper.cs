using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Models.Items;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Data.Entities.Items;

namespace StarColonies.Infrastructures.Mapper.DomainToEntity;

public class ItemToEntityMapper(IDomainToEntityMapper<EffectEntity, EffectModel> effectMapper) : IDomainToEntityMapper<ItemEntity, ItemModel>
{
    public ItemEntity Map(ItemModel entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            CoinsValue = entity.CoinsValue,
            ImagePath = entity.ImagePath,
            EffectId = entity.Effect.Id,
            Effect = effectMapper.Map(entity.Effect)
        };

    public void MapInto(ColonistModel model, ColonistEntity entity)
    {
        throw new NotImplementedException();
    }
}