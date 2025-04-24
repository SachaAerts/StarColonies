using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Models.Missions;

namespace StarColonies.Domains.Repositories;

public interface IMissionRepository
{
    Task<MissionModel> GetMissionByIdAsync(int id);
    Task UpdateMissionAsync(MissionModel updatedModel, IList<int> selectedEnemyIds, IList<RewardItemModel> rewardItems);
}