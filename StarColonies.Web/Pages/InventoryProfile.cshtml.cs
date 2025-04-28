using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Repositories;

namespace StarColonies.Web.Pages;

[Authorize]
public class InventoryProfile(IInventaryRepository inventoryRepository, IItemRepository itemRepository)
    : PageModel
{
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }
    
    public IList<RewardItemModel> Inventory { get; set; } = new List<RewardItemModel>();
    
    public IList<ItemModel> Items { get; set; } = new List<ItemModel>();
    
    public async Task<IActionResult> OnGetAsync()
    {
        Inventory = await inventoryRepository.GetItemsForColonistAsync(Id.ToString());
        Items = await itemRepository.GetAllItemsAsync();
        return Page();
    }
}