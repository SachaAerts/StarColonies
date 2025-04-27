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
        
        return new()
        {
            ItemFactory.Create("Strength Module", "Boosts force slightly.", effectSmallBoostForce.Id, 5, "force.png"),
            ItemFactory.Create("Stamina Pack", "Boosts stamina slightly.", effectSmallBoostStamina.Id, 5, "stamina.png"),
            ItemFactory.Create("Power Amplifier", "Boosts force moderately.", effectMediumBoostForce.Id, 6, "brain.png"),
            ItemFactory.Create("Endurance Battery", "Boosts stamina moderately.", effectMediumBoostStamina.Id, 12, "battery.png"),
            ItemFactory.Create("Adaptative Kit", "Boosts overall capabilities moderately.", effectMediumBoost.Id, 24, "kit.png"),
            ItemFactory.Create("Uncommon Artifact", "An artifact of uncommon origin.", effectUncommon.Id, 46, "gun.png"),
            ItemFactory.Create("Golden Apple", "A Golden Apple rarity, very powerful.", effectEpic.Id, 52, "golden_apple.png"),
            ItemFactory.Create("AK-47", "A core of legendary origin, grants immense power.", effectLegendary.Id, 78, "ak.png")
        };
    }
}