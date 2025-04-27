using Microsoft.EntityFrameworkCore;
using StarColonies.Infrastructures.Data.Seeder.Colonies;
using StarColonies.Infrastructures.Data.Seeder.Colonist;
using StarColonies.Infrastructures.Data.Seeder.Map;

namespace StarColonies.Infrastructures.Data.Seeder;

public class SeedCommand()
{
    private readonly IDataBaseSeeder _colonySeeder = new ColonySeeder();
    private readonly IDataBaseSeeder _mapSeeder = new MapSeeder();
        
    public async Task SeedAsync(StarColoniesDbContext context, IServiceProvider services)
    {
        await context.Database.MigrateAsync();
        
        await IdentitySeeder.SeedRolesAndUsersAsync(services);
        
        _colonySeeder.Seed(context);
        _mapSeeder.Seed(context);
    }
}