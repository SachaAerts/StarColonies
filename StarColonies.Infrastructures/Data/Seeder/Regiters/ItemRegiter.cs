using StarColonies.Infrastructures.Data.Entities.Items;
using StarColonies.Infrastructures.Data.Seeder.Factories;

namespace StarColonies.Infrastructures.Data.Seeder.Regiters;

public class ItemRegiter
{
    public static List<ItemEntity> Register(List<EffectEntity> effects)
    {
        var effectSmallBoostForce = effects.First(e => e.Name == "Small Boost Force");
        var effectSmallBoostStamina = effects.First(e => e.Name == "Small Boost Stamina");
        var effectMediumBoostForce = effects.First(e => e.Name == "Medium Boost Force");
        var effectMediumBoostStamina = effects.First(e => e.Name == "Medium Boost Stamina");
        var effectMediumBoost = effects.First(e => e.Name == "Medium Boost");
        var effectUncommon = effects.First(e => e.Name == "Uncommon");
        var effectEpic = effects.First(e => e.Name == "Epic");
        var effectLegendary = effects.First(e => e.Name == "Legendary");
        var effectLegendary2 = effects.First(e => e.Name == "Legendary2");
        
        return new()
        {
            ItemFactory.Create("Strength Module", "Boosts force slightly.", effectSmallBoostForce.Id, 5, "force.png", false),
            ItemFactory.Create("Stamina Pack", "Boosts stamina slightly.", effectSmallBoostStamina.Id, 5, "stamina.png", false),
            ItemFactory.Create("Power Amplifier", "Boosts force moderately.", effectMediumBoostForce.Id, 6, "brain.png", false),
            ItemFactory.Create("Endurance Battery", "Boosts stamina moderately.", effectMediumBoostStamina.Id, 12, "battery.png", false),
            ItemFactory.Create("Adaptative Kit", "Boosts overall capabilities moderately.", effectMediumBoost.Id, 24, "kit.png", false),
            ItemFactory.Create("Uncommon Artifact", "An artifact of uncommon origin.", effectUncommon.Id, 46, "gun.png", true),
            ItemFactory.Create("Golden Apple", "A Golden Apple rarity, very powerful.", effectEpic.Id, 52, "golden_apple.png", true),
            ItemFactory.Create("AK-47", "A core of legendary origin, grants immense power.", effectLegendary.Id, 78, "ak.png", true),
            ItemFactory.Create("Golden Kebab", "A core of legendary origin", effectLegendary2.Id, 78, "kebab.png", true)
        };
    }
}