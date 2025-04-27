using StarColonies.Domains.Models;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Data.Seeder.Factories;
using StarColonies.Infrastructures.Data.Seeder.Regiters;

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

        var members = CreateColonyMembers(colonies, colonists);
        context.ColonyMember.AddRange(members);
        context.SaveChanges();
    }

    private IList<ColonyMemberEntity> CreateColonyMembers(IList<ColonyEntity> colonies, List<ColonistEntity> colonists)
    {
        var engineers = colonists.Where(c => c.JobModel == JobModel.Engineer).ToList();
        var doctors = colonists.Where(c => c.JobModel == JobModel.Doctor).ToList();
        var scientists = colonists.Where(c => c.JobModel == JobModel.Scientist).ToList();
        var others = colonists.Where(c => c.JobModel == JobModel.Soldier).ToList();

        IList<ColonyMemberEntity> members = new List<ColonyMemberEntity>();
        var random = new Random();

        foreach (var colony in colonies)
        {
            if (engineers.Count == 0 || doctors.Count == 0 || scientists.Count == 0) break;

            var engineer = engineers[random.Next(engineers.Count)];
            var doctor = doctors[random.Next(doctors.Count)];
            var scientist = scientists[random.Next(scientists.Count)];
            var other = others[random.Next(others.Count)];
            
            members.Add(ColonyMemberFactory.Create(colony.Id, engineer.Id));
            members.Add(ColonyMemberFactory.Create(colony.Id, doctor.Id));
            members.Add(ColonyMemberFactory.Create(colony.Id, scientist.Id));
            members.Add(ColonyMemberFactory.Create(colony.Id, other.Id));
        }

        return members;
    }
}
