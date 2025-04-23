using Microsoft.EntityFrameworkCore;
using StarColonies.Infrastructures.Data.Entities.Items;

namespace StarColonies.Infrastructures.Data.Configurations.Seeder;

public class InventarySeeder()
{
    public static void Seed(StarColoniesDbContext context)
    {
        if (context.Inventory.Any()) return;

        var colonists = context.Users
            .OrderBy(u => u.UserName)
            .ToList();

        var items = context.Item.ToList();
        var random = new Random();

        foreach (var colonist in colonists)
        {
            var assignedItems = items
                .OrderBy(_ => random.Next())
                .Take(random.Next(1, 3)) 
                .ToList();

            foreach (var item in assignedItems)
            {
                context.Inventory.Add(new InventoryEntity
                {
                    ColonistId = colonist.Id,
                    ItemId = item.Id,
                    Quantity = random.Next(1, 5)
                });
            }
        }

        context.SaveChanges();
    }
}