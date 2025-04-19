using StarColonies.Domains.Models.Items;
using StarColonies.Infrastructures.Data.Entities.Items;
using StarColonies.Infrastructures.Mapper.EntityToDomain;

namespace StarColonies.Infrastructures.Mapper;

public class EffectToDomainMapper : IEntityToDomainMapper<EffectModel, EffectEntity>
{
    public EffectModel Map(EffectEntity entity)
        => new()
        {
            Id = entity.Id,
            ForceModifier = entity.ForceModifier,
            StaminaModifier = entity.StaminaModifier,
        };
}