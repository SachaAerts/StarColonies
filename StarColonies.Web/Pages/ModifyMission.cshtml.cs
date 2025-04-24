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
    [MissionValidatorService(MaxEnemies = 3, RequireEnemies = false)]
    public List<int> SelectedEnemyIds { get; set; } = [];
    
    [BindProperty]
    public List<int> SelectedItemIds { get; set; } = [];

    [BindProperty]
    public List<int> ItemQuantities { get; set; } = [];
    
    [BindProperty]
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
        if (!ModelState.IsValid) return Page();
        
        if (SelectedItemIds.Count != ItemQuantities.Count)
        {
            ModelState.AddModelError("", "Erreur de synchronisation entre les objets et les quantités.");
            return Page();
        }

        var selectedItems = RewardInputs
            .Where(ri => ri.Selected)
            .ToList();

        if (selectedItems.Count == 0)
        {
            ModelState.AddModelError("", "Vous devez sélectionner au moins un objet de récompense.");
            return Page();
        }

        var rewardModels = new List<RewardItemModel>();
        
        foreach (var ri in selectedItems)
        {
            var item = await itemRepository.GetItemByIdAsync(ri.ItemId);
            if (item != null)
            {
                rewardModels.Add(new RewardItemModel
                {
                    Item = item,
                    Quantity = ri.Quantity
                }); 
            }
        }
        Mission.Items = rewardModels;

        await missionRepository.UpdateMissionAsync(Mission, SelectedEnemyIds, Mission.Items);
        return RedirectToPage("/Map");
    }
    
}

public class RewardInput
{
    public int ItemId { get; set; }
    public bool Selected { get; set; }
    public int Quantity { get; set; }
}

