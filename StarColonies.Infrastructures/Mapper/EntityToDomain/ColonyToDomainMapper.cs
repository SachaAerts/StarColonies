using StarColonies.Domains.Models.Colony;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Mapper.EntityToDomain;

namespace StarColonies.Infrastructures.Mapper.EntityToDomain;

public class ColonyToDomainMapper : IEntityToDomainMapper<ColonyModel, ColonyEntity>
{
    public ColonyModel Map(ColonyEntity entity)
    {
        return new ColonyModel
        {
            Id = entity.Id,
            Name = entity.Name,
            CreatedAt = entity.CreatedAt,
            OwnerId = entity.OwnerId,
            LogoPath = entity.LogoPath,
            Colonists = entity.Members.Select(m => new ColonistModel
            {
                Id = m.Colonist.Id,
                Name = m.Colonist.UserName,
                Email = m.Colonist.Email,
                DateOfBirth = m.Colonist.DateOfBirth,
                ProfilPicture = m.Colonist.ProfilPicture,
                Level = m.Colonist.Level,
                Strength = m.Colonist.Strength,
                Stamina = m.Colonist.Stamina,
                Musty = m.Colonist.Musty,
                Job = m.Colonist.JobModel
            }).ToList()
        };
    }
}