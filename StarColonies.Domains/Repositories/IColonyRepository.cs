using StarColonies.Domains.Models.Colony;

namespace StarColonies.Domains.Repositories;

public interface IColonyRepository
{
    Task<IList<ColonyModel>> GetColoniesForColonistAsync(string colonistId);
    Task<IList<ColonistModel>> GetColonistsForColonyAsync(int colonyId);

    Task AddColonyAsync(ColonyModel model);

    Task<IList<ColonyModel>> GetTop10ColoniesAsync();
}