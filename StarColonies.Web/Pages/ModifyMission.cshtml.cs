using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Models.Missions;
using StarColonies.Domains.Repositories;
using StarColonies.Web.Validators;

namespace StarColonies.Web.Pages;

public class ModifyMission(
    IMissionRepository missionRepository,
    IEnemyRepository enemyRepository,
    IItemRepository itemRepository
    ) : PageModel
{
    [BindProperty]
    public required MissionModel Mission { get; set; }
    
    [BindProperty]
    [MaxEnemies(Max = 3)]
    public List<int> SelectedEnemyIds { get; set; } = [];
    
    [BindProperty]
    public List<int> SelectedItemIds { get; set; } = [];

    [BindProperty]
    public List<int> ItemQuantities { get; set; } = [];
    
    [BindProperty]
    [ValidRewardList]
    public List<RewardInput> RewardInputs { get; set; } = [];

    public IList<ItemModel> Items { get; set; } = new List<ItemModel>();    
    public IList<EnemyModel> Enemies { get; set; } = new List<EnemyModel>();
    
    public async Task<IActionResult> OnGetAsync(int id)
    {
        Mission = await missionRepository.GetMissionByIdAsync(id);
        Enemies = await enemyRepository.GetAllEnemiesListAsync();
        Items   = await itemRepository.GetAllItemsAsync();
        
        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid || !AreItemQuantitiesValid() || !HasSelectedRewards())       
            return await ReturnPageWithReloadedDataAsync();
        
        Mission.Items = await BuildRewardModelsAsync();

        await missionRepository.UpdateMissionAsync(Mission, SelectedEnemyIds, Mission.Items);

        return RedirectToPage("/Map");
    }

    private async Task<IActionResult> ReturnPageWithReloadedDataAsync()
    {
        await ReloadFormDataAsync();
        return Page();
    }

    private bool AreItemQuantitiesValid() => SelectedItemIds.Count == ItemQuantities.Count;
    private bool HasSelectedRewards() => RewardInputs.Any(ri => ri.Selected);

    private async Task<List<RewardItemModel>> BuildRewardModelsAsync()
    {
        var rewardModels = new List<RewardItemModel>();

        var selectedInputs = RewardInputs.Where(ri => ri.Selected).ToList();

        foreach (var ri in selectedInputs)
        {
            var item = await itemRepository.GetItemByIdAsync(ri.ItemId);
            if (item != null) rewardModels.Add(new RewardItemModel { Item = item, Quantity = ri.Quantity });
        }

        return rewardModels;
    }

    
    private async Task ReloadFormDataAsync()
    {
        Enemies = await enemyRepository.GetAllEnemiesListAsync();
        Items = await itemRepository.GetAllItemsAsync();
    }
    
}

public class RewardInput
{
    public int ItemId { get; set; }
    public bool Selected { get; set; }
    public int Quantity { get; set; }
}

