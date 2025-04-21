using StarColonies.Domains.Models.Colony;
using StarColonies.Infrastructures.Data.Entities;

namespace StarColonies.Infrastructures.Mapper.DomainToEntity;

public interface IDomainToEntityMapper<out TE, in TD>
{
    TE Map(TD entity);

    void MapInto(ColonistModel model, ColonistEntity entity);
}