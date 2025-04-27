using StarColonies.Domains.Models.Missions;

namespace StarColonies.Domains.Repositories;

public interface IEnemyRepository
{
    Task<IList<EnemyModel>> GetAllEnemiesListAsync();
}