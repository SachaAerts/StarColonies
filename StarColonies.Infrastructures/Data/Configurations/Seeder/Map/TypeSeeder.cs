using StarColonies.Domains.Models;
using StarColonies.Infrastructures.Data.Entities;

namespace StarColonies.Infrastructures.Data.Configurations.Seeder.Map;

public class TypeSeeder
{
    public static List<TypeEntity> SeedTypes(StarColoniesDbContext context)
    {
        var types = Enum.GetValues(typeof(TypeModel))
            .Cast<TypeModel>()
            .Select(type => new TypeEntity
            {
                Name = type.ToString(),
            })
            .ToList();
        context.Types.AddRange(types);
        context.SaveChanges();
        return types;
    }
}