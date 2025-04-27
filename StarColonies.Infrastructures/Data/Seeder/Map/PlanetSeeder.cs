using StarColonies.Infrastructures.Data.Entities.Missions;

namespace StarColonies.Infrastructures.Data.Seeder.Map;

public class PlanetSeeder
{
    public static IList<PlanetEntity> SeedPlanet(StarColoniesDbContext context)
    {
        var planets = new List<PlanetEntity>
        {
            new() {Name = "Abyssion",  ImagePath = "abyssion.png",  X = 300,  Y = 570  },
            new() {Name = "Gaia Nova", ImagePath = "gaia-nova.png", X = 200,  Y = 200  },
            new() {Name = "Glacius",   ImagePath = "glacius.png",   X = 1500, Y = 300  },
            new() {Name = "Infernis",  ImagePath = "infernis.png",  X = 1780, Y = 1300 },
            new() {Name = "Nyx Prime", ImagePath = "nyx-prime.png", X = 2080, Y = 2000 },
            new() {Name = "Lunaris",   ImagePath = "lunaris.png",   X = 980,  Y = 1500 },
            new() {Name = "Solara",    ImagePath = "solara.png",    X = 2680, Y = 900  },
            new() {Name = "C4-T3rr0",  ImagePath = "red.png",       X = 1000, Y = 1000 },
        };

        context.Planet.AddRange(planets);
        context.SaveChanges();
        return planets;
    }
}