using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Data.Entities.Missions;

namespace StarColonies.Infrastructures.Data.Seeder;

public class ColonySeeder
{
    public static void Seed(StarColoniesDbContext context)
    {
        if (context.Colony.Any()) return;

        var colonists = context.Users
            .OrderBy(u => u.UserName)
            .Take(5)
            .ToList();

        if (colonists.Count < 5) return;

        var colony1 = new ColonyEntity
        {
            Name = "Nova Prime",
            OwnerId = colonists[0].Id,
            Owner = colonists[0],
            MissionExecutions = new List<MissionExecutionEntity>(),
            LogoPath = "default_team_logo.png"
        };

        var colony2 = new ColonyEntity
        {
            Name = "Shadow League",
            OwnerId = colonists[1].Id,
            Owner = colonists[1],
            MissionExecutions = new List<MissionExecutionEntity>(),
            LogoPath = "default_team_logo.png"
        };

        var colony3 = new ColonyEntity
        {
            Name = "Celestial Pact",
            OwnerId = colonists[2].Id,
            Owner = colonists[2],
            MissionExecutions = new List<MissionExecutionEntity>(),
            LogoPath = "default_team_logo.png"
        };

        var colony4 = new ColonyEntity
        {
            Name = "Galactic Alliance",
            OwnerId = colonists[2].Id,
            Owner = colonists[2],
            MissionExecutions = new List<MissionExecutionEntity>(),
            LogoPath = "default_team_logo.png"
        };
        
        var colony5 = new ColonyEntity
        {
            Name = "Stellar Federation",
            OwnerId = colonists[2].Id,
            Owner = colonists[2],
            MissionExecutions = new List<MissionExecutionEntity>(),
            LogoPath = "default_team_logo.png"
        };
        
        var colony6 = new ColonyEntity
        {
            Name = "Lunar Syndicate",
            OwnerId = colonists[2].Id,
            Owner = colonists[2],
            MissionExecutions = new List<MissionExecutionEntity>(),
            LogoPath = "default_team_logo.png"
        };

        var colony7 = new ColonyEntity
        {
            Name = "Solar Dominion",
            OwnerId = colonists[0].Id,
            Owner = colonists[0],
            MissionExecutions = new List<MissionExecutionEntity>(),
            LogoPath = "default_team_logo.png"
        };

        var colony8 = new ColonyEntity
        {
            Name = "Astral Union",
            OwnerId = colonists[0].Id,
            Owner = colonists[0],
            MissionExecutions = new List<MissionExecutionEntity>(),
            LogoPath = "default_team_logo.png"
        };

        var colony9 = new ColonyEntity
        {
            Name = "Nebula Coalition",
            OwnerId = colonists[0].Id,
            Owner = colonists[0],
            MissionExecutions = new List<MissionExecutionEntity>(),
            LogoPath = "default_team_logo.png"
        };

        var colony10 = new ColonyEntity
        {
            Name = "Andromeda Syndicate",
            OwnerId = colonists[0].Id,
            Owner = colonists[0],
            MissionExecutions = new List<MissionExecutionEntity>(),
            LogoPath = "default_team_logo.png"
        };

        context.Colony.AddRange(colony1, colony2, colony3, colony4, colony5, colony6, colony7, colony8, colony9, colony10);
        context.SaveChanges(); 

        var members = new List<ColonyMemberEntity>
        {
            new() { ColonyId = colony1.Id, Colony = colony1, ColonistId = colonists[0].Id, Colonist = colonists[0] },
            new() { ColonyId = colony1.Id, Colony = colony1, ColonistId = colonists[1].Id, Colonist = colonists[1] },

            new() { ColonyId = colony2.Id, Colony = colony2, ColonistId = colonists[1].Id, Colonist = colonists[1] },
            new() { ColonyId = colony2.Id, Colony = colony2, ColonistId = colonists[2].Id, Colonist = colonists[2] },

            new() { ColonyId = colony3.Id, Colony = colony3, ColonistId = colonists[2].Id, Colonist = colonists[2] },
            new() { ColonyId = colony3.Id, Colony = colony3, ColonistId = colonists[3].Id, Colonist = colonists[3] },
            new() { ColonyId = colony3.Id, Colony = colony3, ColonistId = colonists[4].Id, Colonist = colonists[4] },
            
            new() { ColonyId = colony4.Id, Colony = colony4, ColonistId = colonists[0].Id, Colonist = colonists[0] },
            new() { ColonyId = colony4.Id, Colony = colony4, ColonistId = colonists[1].Id, Colonist = colonists[1] },
            
            new() { ColonyId = colony5.Id, Colony = colony5, ColonistId = colonists[2].Id, Colonist = colonists[2] },
            new() { ColonyId = colony5.Id, Colony = colony5, ColonistId = colonists[3].Id, Colonist = colonists[3] },
            new() { ColonyId = colony5.Id, Colony = colony5, ColonistId = colonists[4].Id, Colonist = colonists[4] },
            
            new() { ColonyId = colony6.Id, Colony = colony6, ColonistId = colonists[0].Id, Colonist = colonists[0] },
            
            new() { ColonyId = colony7.Id, Colony = colony7, ColonistId = colonists[1].Id, Colonist = colonists[1] },
            
            new() { ColonyId = colony8.Id, Colony = colony8, ColonistId = colonists[2].Id, Colonist = colonists[2] },
            new() { ColonyId = colony8.Id, Colony = colony8, ColonistId = colonists[3].Id, Colonist = colonists[3] },
            
            new() { ColonyId = colony9.Id, Colony = colony9, ColonistId = colonists[4].Id, Colonist = colonists[4] },
            new() { ColonyId = colony9.Id, Colony = colony9, ColonistId = colonists[0].Id, Colonist = colonists[0] },
            new() { ColonyId = colony9.Id, Colony = colony9, ColonistId = colonists[1].Id, Colonist = colonists[1] },
            new() { ColonyId = colony9.Id, Colony = colony9, ColonistId = colonists[2].Id, Colonist = colonists[2] },
            
            new() { ColonyId = colony10.Id, Colony = colony10, ColonistId = colonists[3].Id, Colonist = colonists[3] },
            new() { ColonyId = colony10.Id, Colony = colony10, ColonistId = colonists[4].Id, Colonist = colonists[4] },
        };

        context.ColonyMember.AddRange(members);
        context.SaveChanges();
    }
}
