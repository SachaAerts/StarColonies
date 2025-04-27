using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Models.Missions;
using StarColonies.Domains.Repositories;
using StarColonies.Web.Services;
using StarColonies.Web.Validators;

namespace StarColonies.Web.Pages;

public class ModifyMission(
    IModifyMissionDataService dataService,
    IModifyMissionExecutionService executionService
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
        Mission = await dataService.GetMissionByIdAsync(id);
        Enemies = await dataService.GetAllEnemiesAsync();
        Items = await dataService.GetAllItemsAsync();

        dataService.InitializeRewardInputs(Mission, Items, RewardInputs);
        dataService.InitializeSelectedEnemyIds(Mission, SelectedEnemyIds);

        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid ||
            !executionService.AreItemQuantitiesValid(SelectedItemIds, ItemQuantities) ||
            !executionService.HasSelectedRewards(RewardInputs))
        {
            await ReloadFormDataAsync();
            return Page();
        }

        var rewardModels = await executionService.BuildRewardModelsAsync(RewardInputs);
        await executionService.UpdateMissionAsync(Mission, SelectedEnemyIds, rewardModels);

        return RedirectToPage("/Map");
    }

    private async Task ReloadFormDataAsync()
    {
        Enemies = await dataService.GetAllEnemiesAsync();
        Items = await dataService.GetAllItemsAsync();
    }
}

public class RewardInput
{
    public int ItemId { get; set; }
    public bool Selected { get; set; }
    public int Quantity { get; set; }
}

