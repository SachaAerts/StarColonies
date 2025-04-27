using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Models.Missions;
using StarColonies.Domains.Repositories;
using StarColonies.Web.Pages;

namespace StarColonies.Web.Services;

public class ModifyMissionDataService(IMissionRepository missionRepository,
    IEnemyRepository enemyRepository,
    IItemRepository itemRepository
) : IModifyMissionDataService
{
    public async Task<MissionModel> GetMissionByIdAsync(int id)
        => await missionRepository.GetMissionByIdAsync(id);

    public async Task<IList<EnemyModel>> GetAllEnemiesAsync()
        => await enemyRepository.GetAllEnemiesListAsync();

    public async Task<IList<ItemModel>> GetAllItemsAsync()
        => await itemRepository.GetAllItemsAsync();

    public void InitializeRewardInputs(MissionModel mission, IList<ItemModel> items, IList<RewardInput> rewardInputs)
    {
        var existingRewards = mission.Items.ToDictionary(item => item.Item.Id, item => item.Quantity);
        rewardInputs = items.Select(item => new RewardInput
        {
            ItemId = item.Id,
            Selected = existingRewards.ContainsKey(item.Id),
            Quantity = existingRewards.GetValueOrDefault(item.Id, 1)
        }).ToList();
    }

    public void InitializeSelectedEnemyIds(MissionModel mission, IList<int> selectedEnemyIds)
        => selectedEnemyIds = mission.Enemies.Select(e => e.Id).ToList();
}