using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Data.Entities.Missions;

namespace StarColonies.Infrastructures.Data.Configurations.Seeder;

public class ColonieSeeder
{
    public static void Seed(StarColoniesDbContext context)
    {
        if (context.Colonies.Any()) return;

        var colonists = context.Users
            .OrderBy(u => u.UserName)
            .Take(5)
            .ToList();

        if (colonists.Count < 5)
        {
            Console.WriteLine("🚫 Not enough colonists to seed colonies.");
            return;
        }

        var colony1 = new ColonieEntity
        {
            Name = "Nova Prime",
            OwnerId = colonists[0].Id,
            Owner = colonists[0],
            MissionExecutions = new List<MissionExecutionEntity>()
        };

        var colony2 = new ColonieEntity
        {
            Name = "Shadow League",
            OwnerId = colonists[1].Id,
            Owner = colonists[1],
            MissionExecutions = new List<MissionExecutionEntity>()
        };

        var colony3 = new ColonieEntity
        {
            Name = "Celestial Pact",
            OwnerId = colonists[2].Id,
            Owner = colonists[2],
            MissionExecutions = new List<MissionExecutionEntity>()
        };

        context.Colonies.AddRange(colony1, colony2, colony3);
        context.SaveChanges(); 

        var members = new List<ColonieMemberEntity>
        {
            new() { ColonieId = colony1.Id, Colonie = colony1, ColonistId = colonists[0].Id, Colonist = colonists[0] },
            new() { ColonieId = colony1.Id, Colonie = colony1, ColonistId = colonists[1].Id, Colonist = colonists[1] },

            new() { ColonieId = colony2.Id, Colonie = colony2, ColonistId = colonists[1].Id, Colonist = colonists[1] },
            new() { ColonieId = colony2.Id, Colonie = colony2, ColonistId = colonists[2].Id, Colonist = colonists[2] },

            new() { ColonieId = colony3.Id, Colonie = colony3, ColonistId = colonists[2].Id, Colonist = colonists[2] },
            new() { ColonieId = colony3.Id, Colonie = colony3, ColonistId = colonists[3].Id, Colonist = colonists[3] },
            new() { ColonieId = colony3.Id, Colonie = colony3, ColonistId = colonists[4].Id, Colonist = colonists[4] }
        };

        context.ColoniesMembers.AddRange(members);
        context.SaveChanges();
    }
}
