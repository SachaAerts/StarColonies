using StarColonies.Domains.Models;
using StarColonies.Infrastructures.Data.Entities;

namespace StarColonies.Infrastructures.Data.Seeder.Map;

public class TypeSeeder
{
    private static List<TypeEntity> SeedTypes(StarColoniesDbContext context)
    {
        var model = new TypeModel();
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