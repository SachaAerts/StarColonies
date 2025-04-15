using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Data.Entities.Items;
using StarColonies.Infrastructures.Data.Entities.Missions;

namespace StarColonies.Infrastructures.Data.Seeder.Map;

public class MapSeeder
{
    public static void Seed(StarColoniesDbContext context)
    {
        if (context.Planets.Any()) return;

        var types = SeedTypes(context);
        var enemies = SeedEnemies(context, types);
        var effects = SeedEffects(context);
        var items = SeedItems(context, effects);
        var planet = SeedPlanet(context);
        
        SeedMission(context, planet, enemies, items);
    }
    
    private static List<TypeEntity> SeedTypes(StarColoniesDbContext context)
    {
        var types = new List<TypeEntity>
        {
            new() { Name = "Robot" },
            new() { Name = "Paranormal" },
            new() { Name = "Extraterrestrial" }
        };

        context.Types.AddRange(types);
        context.SaveChanges();
        return types;
    }

    private static List<EnemyEntity> SeedEnemies(StarColoniesDbContext context, List<TypeEntity> types)
    {
        var enemies = new List<EnemyEntity>
        {
            new() { Name = "Drone", Strength = 2, Stamina = 2, ImagePath = "/Images/Enemies/drone.png", Type = types.First(t => t.Name == "Robot") },
            new() { Name = "Spectre", Strength = 7, Stamina = 1, ImagePath = "/Images/Enemies/spectre.png", Type = types.First(t => t.Name == "Paranormal") },
            new() { Name = "Cryptoid", Strength = 4, Stamina = 12, ImagePath = "/Images/Enemies/cryptoid.png", Type = types.First(t => t.Name == "Extraterrestrial") }
        };

        context.Enemies.AddRange(enemies);
        context.SaveChanges();
        return enemies;
    }
    
    private static List<EffectEntity> SeedEffects(StarColoniesDbContext context)
    {
        var effects = new List<EffectEntity>
        {
            new() { Name = "Boost Force", ForceModifier = 2, StaminaModifier = 0 },
            new() { Name = "Boost Stamina", ForceModifier = 0, StaminaModifier = 3 }
        };

        context.Effects.AddRange(effects);
        context.SaveChanges();
        return effects;
    }
    
    private static List<ItemEntity> SeedItems(StarColoniesDbContext context, List<EffectEntity> effects)
    {
        var items = new List<ItemEntity>
        {
            new() { Name = "Force Module", Description = "Augmente la force", Effect = effects[0], CoinsValue = 10, ImagePath = "/Images/Items/force.png" },
            new() { Name = "Stamina Pack", Description = "Augmente l'endurance", Effect = effects[1], CoinsValue = 8, ImagePath = "/Images/Items/stamina.png" }
        };

        context.Items.AddRange(items);
        context.SaveChanges();
        return items;
    }

    private static PlanetEntity SeedPlanet(StarColoniesDbContext context)
    {
        var planet = new PlanetEntity
        {
            Name = "Earth",
            ImagePath = "/Images/Planets/Earth.png"
        };

        context.Planets.Add(planet);
        context.SaveChanges();
        return planet;
    }

    private static void SeedMission(StarColoniesDbContext context, PlanetEntity planet, List<EnemyEntity> enemies, List<ItemEntity> items)
    {
        var mission = new MissionEntity
        {
            Name = "Exploration de la base abandonnée",
            Difficulty = enemies.Sum(e => e.Strength),
            CoinsReward = 3,
            Planet = planet,
            Enemies = new List<EnemyEntity> { enemies[0], enemies[1], enemies[2] },
            Rewards = new List<RewardedEntity>
            {
                new() { Item = items[0], Quantity = 1 },
                new() { Item = items[1], Quantity = 2 }
            }
        };

        context.Missions.Add(mission);
        context.SaveChanges();
    }
}