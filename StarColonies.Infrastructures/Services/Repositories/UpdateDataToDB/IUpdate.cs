namespace StarColonies.Infrastructures.Services.Repositories.UpdateDataToDB;

public interface IUpdate<T>
{
    Task UpdateAsync(T entity);
}