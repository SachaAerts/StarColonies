using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Data.Entities.Missions;
using StarColonies.Infrastructures.Data.Seeder.Registers;

namespace StarColonies.Infrastructures.Data.Seeder.Map;

public class EnemySeeder
{
    public static List<EnemyEntity> SeedEnemies(StarColoniesDbContext context, List<TypeEntity> types)
    {
        var enemies = EnemyRegister.Register(types);

        context.Enemy.AddRange(enemies);
        context.SaveChanges();
        return enemies;
    }
}