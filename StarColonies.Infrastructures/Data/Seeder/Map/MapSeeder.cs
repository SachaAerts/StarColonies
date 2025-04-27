namespace StarColonies.Infrastructures.Data.Seeder.Map;

public class MapSeeder : IDataBaseSeeder
{
    public void Seed(StarColoniesDbContext context)
    {
        if (context.Planet.Any()) return;

        var types = TypeSeeder.SeedTypes(context);
        
        var enemies = EnemySeeder.SeedEnemies(context, types);
        var effects = EffectSeeder.SeedEffects(context);
        var items = ItemSeeder.SeedItems(context, effects);
        var planets = PlanetSeeder.SeedPlanet(context);
        
        MissionSeeder.SeedMissions(context, planets, enemies, items);
    }
}