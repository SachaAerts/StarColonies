using StarColonies.Domains.Models;

namespace StarColonies.Domains.Repositories;

public interface IPlanetRepository
{
    Task<IList<PlanetModel>> GetPlanetsWithMissionsAsync();
}