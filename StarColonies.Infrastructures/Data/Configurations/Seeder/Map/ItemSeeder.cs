using StarColonies.Infrastructures.Data.Entities.Items;

namespace StarColonies.Infrastructures.Data.Configurations.Seeder.Map;

public class ItemSeeder
{
    public static List<ItemEntity> SeedItems(StarColoniesDbContext context, List<EffectEntity> effects)
    {
        var effect1 = effects.First(e => e.Name == "Boost Force");
        var effect2 = effects.First(e => e.Name == "Boost Stamina");
        var effect3 = effects.First(e => e.Name == "Legendary");
        
        var items = new List<ItemEntity>
        {
            new()
            {
                Name = "Force Module",
                Description = "Augmente la force",
                EffectId = effect1.Id,
                Effect = effect1,
                CoinsValue = 10,
                ImagePath = "/img/items/force.png"
            },
            new()
            {
                Name = "Stamina Pack",
                Description = "Augmente l'endurance",
                EffectId = effect2.Id,
                Effect = effect2,
                CoinsValue = 8,
                ImagePath = "/img/items/stamina.png",
            },
            new()
            {
                Name = "AK-47",
                Description = "Legendary Item",
                EffectId = effect3.Id,
                Effect = effect3,
                CoinsValue = 5,
                ImagePath = "/img/items/ak.png",
            }
        };

        context.SaveChanges();

        return items;
    }
}