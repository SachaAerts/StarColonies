using StarColonies.Infrastructures.Data.Entities.Missions;

namespace StarColonies.Infrastructures.Data.Configurations.Seeder.Map;

public class PlanetSeeder
{
    public static IList<PlanetEntity> SeedPlanet(StarColoniesDbContext context)
    {
        var planets = new List<PlanetEntity>
        {
            new() { Name = "Abyssion",  ImagePath = "/img/planets/abyssion.png",  X = 300,  Y = 570  },
            new() { Name = "Gaia Nova", ImagePath = "/img/planets/gaia-nova.png", X = 200,  Y = 200  },
            new() { Name = "Glacius",   ImagePath = "/img/planets/glacius.png",   X = 1500, Y = 300  },
            new() { Name = "Infernis",  ImagePath = "/img/planets/infernis.png",  X = 1780, Y = 1300 },
            new() { Name = "Nyx Prime", ImagePath = "/img/planets/nyx-prime.png", X = 2080, Y = 2000 },
            new() { Name = "Lunaris",   ImagePath = "/img/planets/lunaris.png",   X = 980,  Y = 1500 },
            new() { Name = "Solara",    ImagePath = "/img/planets/solara.png",    X = 2680, Y = 900  }
        };

        context.Planets.AddRange(planets);
        context.SaveChanges();
        return planets;
    }
}