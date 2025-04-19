using StarColonies.Domains.Models;
using StarColonies.Domains.Models.Missions;
using StarColonies.Infrastructures.Data.Entities.Missions;
using StarColonies.Infrastructures.Mapper.EntityToDomain;

namespace StarColonies.Infrastructures.Mapper;

public class PlanetToDomainMapper(IEntityToDomainMapper<MissionModel, MissionEntity> missionMapper) : IEntityToDomainMapper<PlanetModel, PlanetEntity>
{
    public PlanetModel Map(PlanetEntity entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            X = entity.X,
            Y = entity.Y,
            ImagePath = entity.ImagePath,
            Missions = entity.Missions.Select(missionMapper.Map).ToList()
        };
}