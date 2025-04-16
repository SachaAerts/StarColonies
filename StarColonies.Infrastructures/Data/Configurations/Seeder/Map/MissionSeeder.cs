using StarColonies.Infrastructures.Data.Entities.Items;
using StarColonies.Infrastructures.Data.Entities.Missions;

namespace StarColonies.Infrastructures.Data.Configurations.Seeder.Map;

public class MissionSeeder
{
    public static IList<MissionEntity> SeedMissions(StarColoniesDbContext context, IList<PlanetEntity> planets, List<EnemyEntity> enemies, List<ItemEntity> items)
    {
        IList<MissionEntity> missions = new List<MissionEntity> {
            CreateMission("Exploration de ruines", planets[0], enemies, items),
            
            CreateMission("Survie nocturne", planets[0], enemies, items),

            CreateMission("Expédition florale", planets[1], enemies, items),

            CreateMission("Sonde en détresse", planets[2], enemies, items),

            CreateMission("Nettoyage thermique", planets[3], enemies, items),

            CreateMission("Phénomène noir", planets[4], enemies, items),

            CreateMission("Terraforming partiel", planets[5], enemies, items),

            CreateMission("Extraction de minerai", planets[6], enemies, items),
        };
        
        context.Missions.AddRange(missions);
        context.SaveChanges();

        return missions;
    }
    
    private static MissionEntity CreateMission(string name, PlanetEntity planet, List<EnemyEntity> enemies, List<ItemEntity> items)
    {
        var selectedEnemies = enemies.OrderBy(_ => Guid.NewGuid()).Take(3).ToList();
        var difficulty = selectedEnemies.Sum(e => e.Strength);

        var mission = new MissionEntity
        {
            Name = name,
            Difficulty = difficulty,
            CoinsReward = Random.Shared.Next(1, 4),
            Planet = planet,
            PlanetId = planet.Id,
            Enemies = selectedEnemies,
            Rewards = new List<RewardedEntity>()
        };

        var itemChoices = items.OrderBy(_ => Guid.NewGuid()).Take(2).ToList();

        mission.Rewards.Add(new RewardedEntity
        {
            Item = itemChoices[0],
            ItemId = itemChoices[0].Id,
            MissionId = mission.Id,
            Mission = mission,
            Quantity = 1,
        });

        mission.Rewards.Add(new RewardedEntity
        {
            Item = itemChoices[1],
            ItemId = itemChoices[1].Id,
            MissionId = mission.Id,
            Mission = mission,
            Quantity = 2
        });

        return mission;
    }
}