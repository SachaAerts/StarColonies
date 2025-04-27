using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Models.Missions;
using StarColonies.Domains.Repositories;
using StarColonies.Web.Validators;

namespace StarColonies.Web.Pages;

public class CreateMission(
    IMissionRepository missionRepository,
    IEnemyRepository enemyRepository,
    IItemRepository itemRepository
) : PageModel
{
    [BindProperty]
    public MissionModel Mission { get; set; } = new();

    [BindProperty]
    [Text(Max = 100, Min = 5)]
    public string Title { get; set; }
    
    [BindProperty]
    [Text(Max = 1000, Min = 5)]
    public string Description { get; set; }
    
    [BindProperty]
    [CoinsReward(Max = 1000, Min = 1)]
    public int CoinsReward { get; set; }
    
    [BindProperty]
    [MaxEnemies(Max = 3)]
    public List<int> SelectedEnemyIds { get; set; } = [];

    [BindProperty]
    [ValidRewardList]
    public List<RewardInput> RewardInputs { get; set; } = [];

    public IList<ItemModel> Items { get; set; } = new List<ItemModel>();
    public IList<EnemyModel> Enemies { get; set; } = new List<EnemyModel>();
    
    [BindProperty(SupportsGet = true)]
    public int PlanetId { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        PlanetId = id;
        await LoadFormDataAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid || !RewardInputs.Any(r => r.Selected))
        {
            await LoadFormDataAsync();
            return Page();
        }
        Mission.Name = Title;
        Mission.Description = Description;
        Mission.CoinsReward = CoinsReward;
        Mission.Items = await BuildRewardModelsAsync();
        await missionRepository.CreateMissionAsync(PlanetId, Mission, SelectedEnemyIds, Mission.Items);

        return RedirectToPage("/Map");
    }

    private async Task LoadFormDataAsync()
    {
        Enemies = await enemyRepository.GetAllEnemiesListAsync();
        Items = await itemRepository.GetAllItemsAsync();
    }

    private async Task<List<RewardItemModel>> BuildRewardModelsAsync()
    {
        var rewardModels = new List<RewardItemModel>();
        var selected = RewardInputs.Where(ri => ri.Selected).ToList();

        foreach (var ri in selected)
        {
            var item = await itemRepository.GetItemByIdAsync(ri.ItemId);
            if (item != null) rewardModels.Add(new RewardItemModel { Item = item, Quantity = ri.Quantity });
        }

        return rewardModels;
    }
}
