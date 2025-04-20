using Microsoft.EntityFrameworkCore;
using StarColonies.Domains.Models;
using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Repositories;
using StarColonies.Infrastructures.Data;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Data.Entities.Missions;
using StarColonies.Infrastructures.Mapper;
using StarColonies.Infrastructures.Mapper.EntityToDomain;

namespace StarColonies.Infrastructures.Repositories;

public class MapRepository(
    StarColoniesDbContext context,
    IEntityToDomainMapper<PlanetModel, PlanetEntity> planetMapper) : IMapRepository
{
    public async Task<IList<PlanetModel>> GetPlanetsWithMissionsAsync()
    {
        var planets = await context.Planets
            .Include(p => p.Missions)
                .ThenInclude(m => m.Enemies)
                    .ThenInclude(e => e.Type)
            .Include(p => p.Missions)
                .ThenInclude(m => m.Rewards)
                    .ThenInclude(r => r.Item)
                        .ThenInclude(i => i.Effect)
            .ToListAsync();

        return planets.Select(planetMapper.Map).ToList();
    }
}
