namespace StarColonies.Infrastructures.Services.Repositories.AddingDataToDB;

public interface IAdding<in T>
{
    Task AddAsync(T entity);
}