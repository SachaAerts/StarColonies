using StarColonies.Domains.Models.Colony;

namespace StarColonies.Domains.Repositories;

public interface IColonistRepository
{
    Task<IList<ColonistModel>> GetColonistsAsync();
    
    Task<ColonistModel> GetColonistByIdAsync(string id);
    Task<ColonistModel> GetColonistByNameAsync(string name);
    
    Task AddColonistAsync(ColonistModel colonist);
    Task UpdateColonistAsync(ColonistModel colonistModel);
    Task DeleteColonistAsync(string id);
    
    Task DebitColonistAsync(string id, int amount);
    
    Task AddMustyColonistAsync(string id, int amount);
}