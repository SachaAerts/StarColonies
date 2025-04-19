using StarColonies.Domains.Models.Colony;
using StarColonies.Infrastructures.Data.Entities;

namespace StarColonies.Infrastructures.Mapper.EntityToDomain;

public class ColonistToDomainMapper : IEntityToDomainMapper<ColonistModel, ColonistEntity>
{
    public ColonistModel Map(ColonistEntity entity)
    => new()
        {
            Id = entity.Id,
            Name = entity.UserName,
            ProfilPicture = entity.ProfilPicture,
            Job = entity.JobModel,
            Strength = entity.Strength,
            Stamina = entity.Stamina,
            Musty = entity.Musty,
            DateOfBirth = entity.DateOfBirth,
            Level = entity.Level,
            Email = entity.Email
        };
}