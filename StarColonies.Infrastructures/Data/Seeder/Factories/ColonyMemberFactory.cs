using StarColonies.Infrastructures.Data.Entities;

namespace StarColonies.Infrastructures.Data.Seeder.Factories;

public class ColonyMemberFactory
{
    public static ColonyMemberEntity Create(int colonyId, string colonistsId)
        => new()
            {
                ColonyId = colonyId,
                ColonistId = colonistsId,
            };
}