using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Data.Entities.Missions;

namespace StarColonies.Infrastructures.Data.Configurations.Seeder.Map;

public class EnemySeeder
{
    public static List<EnemyEntity> SeedEnemies(StarColoniesDbContext context, List<TypeEntity> types)
    {
        var enemies = new List<EnemyEntity>
        {
            new()
            {
                Name = "Drone",     
                Strength = 2, Stamina = 2,
                ImagePath = "/Images/Enemies/drone.png",
                Type = types.First(t => t.Name == "Robot"),
            },
            new()
            {
                Name = "Spectre",   
                Strength = 7, Stamina = 1,
                ImagePath = "/Images/Enemies/spectre.png",
                Type = types.First(t => t.Name == "Paranormal"),
            },
            new()
            {
                Name = "Cryptoid", 
                Strength = 4, Stamina = 12, 
                ImagePath = "/Images/Enemies/cryptoid.png",
                Type = types.First(t => t.Name == "Extraterrestrial"),
            }
        };

        context.Enemies.AddRange(enemies);
        context.SaveChanges();
        return enemies;
    }
}