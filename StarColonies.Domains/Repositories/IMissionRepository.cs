using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Models.Missions;

namespace StarColonies.Domains.Repositories;

public interface IMissionRepository
{
    Task<MissionModel> GetMissionByIdAsync(int id);
    Task MissionExecute(int id, int colonyId, MissionResultModel result);
    Task CreateMissionAsync(int planetId, MissionModel mission, IList<int> selectedEnemyIds, IList<RewardItemModel> rewardItems);
    Task UpdateMissionAsync(MissionModel updatedModel, IList<int> selectedEnemyIds, IList<RewardItemModel> rewardItems);
    Task VisibleMissionAsync(int id);
}