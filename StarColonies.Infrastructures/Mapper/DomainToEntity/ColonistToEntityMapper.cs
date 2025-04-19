using StarColonies.Domains.Models.Colony;
using StarColonies.Infrastructures.Data.Entities;

namespace StarColonies.Infrastructures.Mapper.DomainToEntity;

public class ColonistToEntityMapper : IDomainToEntityMapper<ColonistEntity, ColonistModel>
{
    public ColonistEntity Map(ColonistModel model)
        => new()
        {
            Id = model.Id,
            UserName = model.Name,
            Email = model.Email,
            DateOfBirth = model.DateOfBirth,
            JobModel = model.Job,
            Level = model.Level,
            Strength = model.Strength,
            Stamina = model.Stamina,
            Musty = model.Musty,
            ProfilPicture = model.ProfilPicture
        };
}