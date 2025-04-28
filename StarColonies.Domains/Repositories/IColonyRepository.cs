using StarColonies.Domains.Models.Colony;

namespace StarColonies.Domains.Repositories;

public interface IColonyRepository
{
    Task<IList<ColonyModel>> GetColoniesForColonistAsync(string colonistId);
    
    Task<IList<ColonistModel>> GetColonistsForColonyAsync(int colonyId);

    Task<IList<ColonyModel>> GetTop10ColoniesAsync();
    
    Task AddColonyAsync(ColonyModel model);
    
    Task<ColonyModel?> GetColonyByIdAsync(int colonyId);

    Task DeleteColonyAsync(int colonyId);

    Task UpdateColonyAsync(ColonyModel modifyColony);
}