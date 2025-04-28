using StarColonies.Domains.Models;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Data.Seeder.Factories;

namespace StarColonies.Infrastructures.Data.Seeder.Colonies;

public static class ColonyMemberSeeder
{
    private static IList<ColonistEntity> _engineers = new List<ColonistEntity>();
    private static IList<ColonistEntity> _doctors = new List<ColonistEntity>();
    private static IList<ColonistEntity> _scientists = new List<ColonistEntity>();
    private static IList<ColonistEntity> _others = new List<ColonistEntity>();
    
    public static IList<ColonyMemberEntity> CreateColonyMembers(IList<ColonyEntity> colonies, List<ColonistEntity> colonists)
    {
        LoadResource(colonists);
        
        IList<ColonyMemberEntity> members = new List<ColonyMemberEntity>();
    
        foreach (var colony in colonies)
        {
            if (_engineers.Count == 0 || _doctors.Count == 0 || _scientists.Count == 0) break;
            
            members.Add(ColonyMemberFactory.Create(colony.Id, GetRandomColonist(_engineers).Id));
            members.Add(ColonyMemberFactory.Create(colony.Id, GetRandomColonist(_doctors).Id));
            members.Add(ColonyMemberFactory.Create(colony.Id, GetRandomColonist(_scientists).Id));
            members.Add(ColonyMemberFactory.Create(colony.Id, GetRandomColonist(_others).Id));
        }

        return members;
    }

    private static void LoadResource(IList<ColonistEntity> colonists)
    {
        _engineers = colonists.Where(c => c.JobModel == JobModel.Engineer).ToList();
        _doctors = colonists.Where(c => c.JobModel == JobModel.Doctor).ToList();
        _scientists = colonists.Where(c => c.JobModel == JobModel.Scientist).ToList();
        _others = colonists.Where(c => c.JobModel == JobModel.Soldier).ToList();
    }

    private static ColonistEntity GetRandomColonist(IList<ColonistEntity> colonists)
    {
        var random = new Random();
        return colonists[random.Next(colonists.Count)];
    }
    
}