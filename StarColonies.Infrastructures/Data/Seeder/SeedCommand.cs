using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StarColonies.Infrastructures.Data.Configurations.Seeder;
using StarColonies.Infrastructures.Data.Configurations.Seeder.Map;

namespace StarColonies.Infrastructures.Data.Seeder;

public static class SeedCommand
{
    public static async Task SeedAsync(StarColoniesDbContext context)
    {
        await context.Database.MigrateAsync();
        MapSeeder.Seed(context);
        ColonySeeder.Seed(context);
        InventarySeeder.Seed(context);
    }
}