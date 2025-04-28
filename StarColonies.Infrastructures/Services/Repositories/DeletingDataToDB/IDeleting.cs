namespace StarColonies.Infrastructures.Services.Repositories.DeletingDataToDB;

public interface IDeleting<T>
{
    Task DeleteEntityAsync(string id, T entity);
}