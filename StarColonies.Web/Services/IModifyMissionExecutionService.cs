using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Models.Missions;
using StarColonies.Web.Pages;

namespace StarColonies.Web.Services;

public interface IModifyMissionExecutionService
{
    bool AreItemQuantitiesValid(IList<int> selectedItemIds, IList<int> itemQuantities);
    bool HasSelectedRewards(IList<RewardInput> rewardInputs);
    Task<IList<RewardItemModel>> BuildRewardModelsAsync(IList<RewardInput> rewardInputs);
    Task UpdateMissionAsync(MissionModel mission, IList<int> selectedEnemyIds, IList<RewardItemModel> rewardModels);
}