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
                Strength = 2,
                Stamina = 2,
                ImagePath = "/img/enemies/drone.png",
                Type = types.First(t => t.Name == "Robot"),
            },
            new()
            {
                Name = "Anomalie",
                Strength = 1,
                Stamina = 3,
                ImagePath = "/img/enemies/anomalie.png",
                Type = types.First(t => t.Name == "Natural"),
            },
            new()
            {
                Name = "Cryptoid",
                Strength = 4,
                Stamina = 12,
                ImagePath = "/img/enemies/cryptoid.png",
                Type = types.First(t => t.Name == "Extraterrestrial"),
            },
            new()
            {
                Name = "Spectre",
                Strength = 7,
                Stamina = 1,
                ImagePath = "/img/enemies/spectre.png",
                Type = types.First(t => t.Name == "Paranormal"),
            },
            new()
            {
                Name = "Nanobot",
                Strength = 1,
                Stamina = 2,
                ImagePath = "/img/enemies/nanobot.png",
                Type = types.First(t => t.Name == "Robot"),
            },
            new()
            {
                Name = "Prédateur",
                Strength = 4,
                Stamina = 3,
                ImagePath = "/img/enemies/predateur.png",
                Type = types.First(t => t.Name == "Animal"),
            },
            new()
            {
                Name = "Chimère",
                Strength = 4,
                Stamina = 4,
                ImagePath = "/img/enemies/chimere.png",
                Type = types.First(t => t.Name == "Experiment"),
            },
            new()
            {
                Name = "Titan",
                Strength = 5,
                Stamina = 3,
                ImagePath = "/img/enemies/titan.png",
                Type = types.First(t => t.Name == "Extraterrestrial"),
            },
            new()
            {
                Name = "Entité",
                Strength = 5,
                Stamina = 11,
                ImagePath = "/img/enemies/entite.png",
                Type = types.First(t => t.Name == "Paranormal"),
            },
            new()
            {
                Name = "Mutant",
                Strength = 2,
                Stamina = 4,
                ImagePath = "/img/enemies/mutant.png",
                Type = types.First(t => t.Name == "Humanoid"),
            },
            new()
            {
                Name = "Leviathan",
                Strength = 9,
                Stamina = 7,
                ImagePath = "/img/enemies/leviathan.png",
                Type = types.First(t => t.Name == "Extraterrestrial"),
            },
            new()
            {
                Name = "Hégémon",
                Strength = 20,
                Stamina = 20,
                ImagePath = "/img/enemies/hegemon.png",
                Type = types.First(t => t.Name == "Extraterrestrial"),
            },
            new()
            {
                Name = "Drone de combat",
                Strength = 10,
                Stamina = 15,
                ImagePath = "/img/enemies/drone_de_combat.png",
                Type = types.First(t => t.Name == "Robot"),
            }
        };

        context.Enemy.AddRange(enemies);
        context.SaveChanges();
        return enemies;
    }
}