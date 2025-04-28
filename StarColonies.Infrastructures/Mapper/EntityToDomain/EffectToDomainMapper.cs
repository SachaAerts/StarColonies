using StarColonies.Domains.Models.Items;
using StarColonies.Infrastructures.Data.Entities.Items;

namespace StarColonies.Infrastructures.Mapper.EntityToDomain;

public class EffectToDomainMapper : IEntityToDomainMapper<EffectModel, EffectEntity>
{
    public EffectModel Map(EffectEntity? entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        return new EffectModel
        {
            Id = entity.Id,
            ForceModifier = entity.ForceModifier,
            StaminaModifier = entity.StaminaModifier,
        };
    }
}