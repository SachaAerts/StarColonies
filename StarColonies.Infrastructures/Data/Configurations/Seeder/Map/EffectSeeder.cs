using StarColonies.Infrastructures.Data.Entities.Items;

namespace StarColonies.Infrastructures.Data.Configurations.Seeder.Map;

public class EffectSeeder
{

    public static List<EffectEntity> SeedEffects(StarColoniesDbContext context)
    {
        var effects = new List<EffectEntity>
        {
            new() { Name = "Boost Force",   ForceModifier = 2, StaminaModifier = 0 },
            new() { Name = "Boost Stamina", ForceModifier = 0, StaminaModifier = 3 }
        };

        context.Effects.AddRange(effects);
        context.SaveChanges();
        return effects;
    }

}