using StarColonies.Infrastructures.Data.Entities.Items;
using StarColonies.Infrastructures.Data.Entities.Missions;
using StarColonies.Infrastructures.Data.Seeder.Registers;

namespace StarColonies.Infrastructures.Data.Seeder.Map;

public class MissionSeeder
{
    public static void SeedMissions(StarColoniesDbContext context, IList<PlanetEntity> planets,
        List<EnemyEntity> enemies, List<ItemEntity> items)
    {
        IList<MissionEntity> missions = MissionRegister.Register(planets, enemies, items);

        context.Mission.AddRange(missions);
        context.SaveChanges();
    }
}