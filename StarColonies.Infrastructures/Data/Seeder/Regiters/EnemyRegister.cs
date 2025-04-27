using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Data.Entities.Missions;
using StarColonies.Infrastructures.Data.Seeder.Factories;

namespace StarColonies.Infrastructures.Data.Seeder.Regiters;

public class EnemyRegister
{
    public static List<EnemyEntity> Register(List<TypeEntity> types)
    => new()
        {
            EnemyFactory.Create("Drone", 2, 2, "drone.png", types.First(t => t.Name == "Robot")),
            EnemyFactory.Create("Anomalie", 1, 3, "anomalie.png", types.First(t => t.Name == "Natural")),
            EnemyFactory.Create("Cryptoid", 4, 12, "cryptoid.png", types.First(t => t.Name == "Extraterrestrial")),
            EnemyFactory.Create("Spectre", 7, 1, "spectre.png", types.First(t => t.Name == "Paranormal")),
            EnemyFactory.Create("Nanobot", 1, 2, "nanobot.png", types.First(t => t.Name == "Robot")),
            EnemyFactory.Create("Prédateur", 4, 3, "predateur.png", types.First(t => t.Name == "Animal")),
            EnemyFactory.Create("Chimère", 4, 4, "chimere.png", types.First(t => t.Name == "Experiment")),
            EnemyFactory.Create("Titan", 5, 3, "titan.png", types.First(t => t.Name == "Extraterrestrial")),
            EnemyFactory.Create("Entité", 5, 11, "entite.png", types.First(t => t.Name == "Paranormal")),
            EnemyFactory.Create( "Mutant", 2, 4, "mutant.png", types.First(t => t.Name == "Humanoid")),
            EnemyFactory.Create( "Leviathan", 9, 7, "leviathan.png", types.First(t => t.Name == "Extraterrestrial")),
            EnemyFactory.Create( "Hégémon", 13, 10, "hegemon.png", types.First(t => t.Name == "Extraterrestrial")),
            EnemyFactory.Create( "Drone de combat", 20, 15, "drone_de_combat.png", types.First(t => t.Name == "Robot")),
            
            EnemyFactory.Create( "Ayoub", 30, 40, "arabe.png", types.First(t => t.Name == "Humanoid")),
            EnemyFactory.Create( "Ilhan", 30, 40, "turc.png", types.First(t => t.Name == "Humanoid"))
        };
}