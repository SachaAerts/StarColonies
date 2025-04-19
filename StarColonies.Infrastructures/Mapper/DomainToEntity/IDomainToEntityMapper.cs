namespace StarColonies.Infrastructures.Mapper.DomainToEntity;

public interface IDomainToEntityMapper<out TE, in TD>
{
    TE Map(TD entity);
}