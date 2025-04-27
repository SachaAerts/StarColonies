using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Data.Entities.Missions;

namespace StarColonies.Infrastructures.Data.Seeder.Factories;

public class ColonyFactory
{
    
    public static ColonyEntity Create(string name, string ownerId, string logo)
        => new ()
            {
                Name = name,
                OwnerId = ownerId,
                MissionExecutions = new List<MissionExecutionEntity>(),
                LogoPath = logo
            };
}