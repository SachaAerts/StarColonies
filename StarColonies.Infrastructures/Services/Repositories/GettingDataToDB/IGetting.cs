using StarColonies.Domains.Models.Colony;

namespace StarColonies.Infrastructures.Services.Repositories.GettingDataToDB;

public interface IGetting<T, in TId>
{
    Task<IList<T>> GetIListDataAsync();
    
    Task<T> GetEntityByIdAsync(TId id);
}