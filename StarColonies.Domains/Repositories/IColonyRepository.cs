using StarColonies.Domains.Models.Colony;

namespace StarColonies.Domains.Repositories;

public interface IColonyRepository
{
    Task<IList<ColonyModel>> GetColoniesForColonistAsync(string colonistId);
    Task<IList<ColonistModel>> GetColonistsForColonyAsync(int colonyId);
    
    void AddColony(ColonyModel colony);

    Task<IList<ColonyModel>> GetTop10ColoniesAsync();
}