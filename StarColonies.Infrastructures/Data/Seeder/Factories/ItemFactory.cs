using Microsoft.EntityFrameworkCore.Migrations.Operations;
using StarColonies.Infrastructures.Data.Entities.Items;

namespace StarColonies.Infrastructures.Data.Seeder.Factories;

public static class ItemFactory
{
    public static ItemEntity Create(string name, string description, int effectId, int coinsValue, string image, bool isLegendary)
        => new ()
        {
            Name = name,
            Description = description,
            EffectId = effectId,
            CoinsValue = coinsValue,
            ImagePath = image,
            NumberOfBuy = Random.Shared.Next(1, 50),
            isLegendary = isLegendary
        };
}