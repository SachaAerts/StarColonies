using StarColonies.Domains.Models.Colony;

namespace StarColonies.Domains.Repositories;

public interface IColonistRepository
{
    Task AddColonistAsync(ColonistModel colonist);
    
    Task<IList<ColonistModel>> GetColonistsAsync();
    
    Task<ColonistModel> GetColonistByIdAsync(string id);
    
    Task UpdateColonistAsync(ColonistModel colonistModel);
}