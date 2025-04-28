using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Data.Seeder.Enum;
using StarColonies.Infrastructures.Data.Seeder.Factories;

namespace StarColonies.Infrastructures.Data.Seeder.Registers;

public static class ColonyRegister
{
    public static List<ColonyEntity> Register(List<ColonistEntity> colonists)
        =>
        [
            ColonyFactory.Create(ColonyNames.AstralEmpire.ToString(), colonists[0].Id, "default_team_logo.png"),
            ColonyFactory.Create(ColonyNames.NovaPrime.ToString(), colonists[0].Id, "default_team_logo.png"),
            ColonyFactory.Create(ColonyNames.StellarFederation.ToString(), colonists[0].Id, "default_team_logo.png"),
            ColonyFactory.Create(ColonyNames.CelestialPact.ToString(), colonists[1].Id, "default_team_logo.png"),
            ColonyFactory.Create(ColonyNames.QuantumCollective.ToString(), colonists[1].Id, "default_team_logo.png"),
            ColonyFactory.Create(ColonyNames.CosmicUnion.ToString(), colonists[2].Id, "default_team_logo.png"),
            ColonyFactory.Create(ColonyNames.IntergalacticEmpire.ToString(), colonists[2].Id, "default_team_logo.png"),
            ColonyFactory.Create(ColonyNames.NebulaFrontier.ToString(), colonists[3].Id, "default_team_logo.png"),
            ColonyFactory.Create(ColonyNames.InterstellarConfederation.ToString(), colonists[3].Id, "default_team_logo.png"),
            ColonyFactory.Create( ColonyNames.ShadowLeague.ToString(), colonists[3].Id, "default_team_logo.png"),
            ColonyFactory.Create( ColonyNames.GalacticAlliance.ToString(), colonists[4].Id, "default_team_logo.png"),
        ];
}