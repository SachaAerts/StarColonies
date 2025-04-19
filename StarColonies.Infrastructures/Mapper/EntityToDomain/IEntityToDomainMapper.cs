namespace StarColonies.Infrastructures.Mapper.EntityToDomain;

public interface IEntityToDomainMapper<out TD, in TE>
{
    TD Map(TE entity);
}