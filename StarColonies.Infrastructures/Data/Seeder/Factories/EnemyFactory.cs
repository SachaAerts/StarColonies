using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Data.Entities.Missions;

namespace StarColonies.Infrastructures.Data.Seeder.Factories;

public class EnemyFactory
{
    public static EnemyEntity Create(string name, int strength, int stamina, string image, TypeEntity type)
    {
        return new EnemyEntity()
        {
            Name = name,
            Strength = strength,
            Stamina = stamina,
            ImagePath = image,
            TypeId = type.Id,
            Type = type,
        };
    }
}