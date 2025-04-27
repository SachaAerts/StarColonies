using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Models.Missions;
using StarColonies.Web.Pages;

namespace StarColonies.Web.Services;

public interface IModifyMissionDataService
{
    Task<MissionModel> GetMissionByIdAsync(int id);
    Task<IList<EnemyModel>> GetAllEnemiesAsync();
    Task<IList<ItemModel>> GetAllItemsAsync();
    void InitializeRewardInputs(MissionModel mission, IList<ItemModel> items, IList<RewardInput> rewardInputs);
    void InitializeSelectedEnemyIds(MissionModel mission, IList<int> selectedEnemyIds);
}