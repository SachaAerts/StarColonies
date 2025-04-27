using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Models.Missions;
using StarColonies.Domains.Repositories;
using StarColonies.Web.Pages;

namespace StarColonies.Web.Services;

public class ModifyMissionExecutionService(
    IMissionRepository missionRepository,
    IItemRepository itemRepository
) : IModifyMissionExecutionService
{
    public bool AreItemQuantitiesValid(IList<int> selectedItemIds, IList<int> itemQuantities)
        => selectedItemIds.Count == itemQuantities.Count;

    public bool HasSelectedRewards(IList<RewardInput> rewardInputs)
        => rewardInputs.Any(ri => ri.Selected);

    public async Task<IList<RewardItemModel>> BuildRewardModelsAsync(IList<RewardInput> rewardInputs)
    {
        var rewardModels = new List<RewardItemModel>();

        var selectedInputs = rewardInputs.Where(ri => ri.Selected).ToList();

        foreach (var ri in selectedInputs)
        {
            var item = await itemRepository.GetItemByIdAsync(ri.ItemId);
            if (item != null) rewardModels.Add(new RewardItemModel { Item = item, Quantity = ri.Quantity });
        }

        return rewardModels;
    }

    public async Task UpdateMissionAsync(MissionModel mission, IList<int> selectedEnemyIds, IList<RewardItemModel> rewardModels)
    {
        mission.Items = rewardModels;
        await missionRepository.UpdateMissionAsync(mission, selectedEnemyIds, rewardModels);
    }
}