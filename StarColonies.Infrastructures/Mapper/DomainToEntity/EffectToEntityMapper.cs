using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Models.Items;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Data.Entities.Items;

namespace StarColonies.Infrastructures.Mapper.DomainToEntity;

public class EffectToEntityMapper : IDomainToEntityMapper<EffectEntity, EffectModel>
{
    public EffectEntity Map(EffectModel entity)
        => new()
        {
            Id = entity.Id,
            StaminaModifier = entity.StaminaModifier,
            ForceModifier = entity.ForceModifier
        };
    
    public void MapInto(ColonistModel model, ColonistEntity entity)
    {
        throw new NotImplementedException();
    }
}