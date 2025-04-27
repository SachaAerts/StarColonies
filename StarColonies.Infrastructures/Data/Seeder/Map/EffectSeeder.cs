using StarColonies.Infrastructures.Data.Entities.Items;

namespace StarColonies.Infrastructures.Data.Seeder.Map;

public class EffectSeeder
{
    public static List<EffectEntity> SeedEffects(StarColoniesDbContext context)
    {
        var effects = new List<EffectEntity>
        {
            new() { Name = "Small Boost Force",     ForceModifier = 2,  StaminaModifier = 0 },
            new() { Name = "Small Boost Stamina",   ForceModifier = 0,  StaminaModifier = 3 },
            new() { Name = "Medium Boost Force",    ForceModifier = 5,  StaminaModifier = 0 },
            new() { Name = "Medium Boost Stamina",  ForceModifier = 0,  StaminaModifier = 5 },
            new() { Name = "Medium Boost",          ForceModifier = 5,  StaminaModifier = 5 },
            new() { Name = "Uncommon",              ForceModifier = 2, StaminaModifier = 20 },
            new() { Name = "Epic",                  ForceModifier = 15,  StaminaModifier = 5 },
            new() { Name = "Legendary",             ForceModifier = 50, StaminaModifier = 30 },
            new() { Name = "Legendary2",            ForceModifier = 90, StaminaModifier = 0 },
        };

        context.Effect.AddRange(effects);
        context.SaveChanges();
        return effects;
    }

}