using StarColonies.Domains.Models;
using StarColonies.Domains.Models.Colony;

namespace StarColonies.Domains.Repositories;

public interface IMapRepository
{
    Task<IList<PlanetModel>> GetPlanetsWithMissionsAsync();
}