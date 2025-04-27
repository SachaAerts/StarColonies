using StarColonies.Infrastructures.Data.Entities.Items;
using StarColonies.Infrastructures.Data.Entities.Missions;

namespace StarColonies.Infrastructures.Data.Seeder.Factories;

public class MissionFactory
{
    public static MissionEntity Create(string name, string description, int coins, PlanetEntity planet,
        IList<EnemyEntity> enemies, IList<RewardedEntity> items)
    {
        return new MissionEntity
        {
            Name = name,
            Description = description,
            Planet = planet,
            Enemies = enemies,
            Difficulty = enemies.Sum(e => e.Strength),
            Rewards = items,
            CoinsReward = coins,
            PlanetId = planet.Id,
        };
    }
}