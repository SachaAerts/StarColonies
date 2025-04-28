using StarColonies.Domains.Models.Colony;

namespace StarColonies.Infrastructures.Services.Repositories.GettingDataToDB;

public interface IGetting<T>
{
    Task<IList<T>> GetIListDataAsync();
    
    Task<T> GetEntityByIdAsync(string id);
}