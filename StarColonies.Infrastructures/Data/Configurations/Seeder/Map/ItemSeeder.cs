using StarColonies.Infrastructures.Data.Entities.Items;

namespace StarColonies.Infrastructures.Data.Configurations.Seeder.Map;

public class ItemSeeder
{
    public static List<ItemEntity> SeedItems(StarColoniesDbContext context, List<EffectEntity> effects)
    {
        var effect1 = effects.First(e => e.Name == "Boost Force");
        var effect2 = effects.First(e => e.Name == "Boost Stamina");
        var items = new List<ItemEntity>
        {
            new()
            {
                Name = "Force Module",
                Description = "Augmente la force",
                EffectId = effect1.Id,
                Effect = effect1,
                CoinsValue = 10,
                ImagePath = "/Images/Items/force.png",
            },
            new()
            {
                Name = "Stamina Pack",
                Description = "Augmente l'endurance",
                EffectId = effect2.Id,
                Effect = effect2,
                CoinsValue = 8,
                ImagePath = "/Images/Items/stamina.png",
            }
        };

        context.Items.AddRange(items);
        context.SaveChanges();
        return items;
    }
}