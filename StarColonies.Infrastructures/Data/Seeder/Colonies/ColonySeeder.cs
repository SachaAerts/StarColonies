using StarColonies.Domains.Models;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Data.Seeder.Factories;
using StarColonies.Infrastructures.Data.Seeder.Registers;

namespace StarColonies.Infrastructures.Data.Seeder.Colonies;

public class ColonySeeder : IDataBaseSeeder
{
    public void Seed(StarColoniesDbContext context)
    {
        if (context.Colony.Any()) return;

        var colonists = context.Users
            .OrderBy(u => u.UserName)
            .ToList();

        if (colonists.Count < 5) return;

        var colonies = ColonyRegister.Register(colonists);
        context.Colony.AddRange(colonies);
        context.SaveChanges();

        var members = ColonyMemberSeeder.CreateColonyMembers(colonies, colonists);
        context.ColonyMember.AddRange(members);
        context.SaveChanges();
    } 
}
