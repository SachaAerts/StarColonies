using StarColonies.Infrastructures.Data.Entities.Items;
using StarColonies.Infrastructures.Data.Seeder.Regiters;

namespace StarColonies.Infrastructures.Data.Seeder.Map;

public class ItemSeeder
{
    public static List<ItemEntity> SeedItems(StarColoniesDbContext context, List<EffectEntity> effects)
    {
        var items = ItemRegiter.Register(effects);
        context.Item.AddRange(items);
        context.SaveChanges();
        return items;
    }
}