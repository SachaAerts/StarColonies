using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Repositories;
using StarColonies.Infrastructures.Data.Entities;

namespace StarColonies.Web.Pages;

[Authorize]
public class InventoryProfile(UserManager<ColonistEntity> userManager, IInventaryRepository inventoryRepository, IItemRepository itemRepository)
    : PageModel
{
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }
    
    public IList<RewardItemModel> Inventory { get; set; } = new List<RewardItemModel>();
    
    public IList<ItemModel> Items { get; set; } = new List<ItemModel>();
    
    public async Task<IActionResult> OnGetAsync()
    {
        var user = await userManager.GetUserAsync(HttpContext.User);
        if (user!.Id != Id.ToString())
        {
            return RedirectToPage("Index");
        }
        Inventory = await inventoryRepository.GetItemsForColonistAsync(Id.ToString());
        Items = await itemRepository.GetAllItemsAsync();
        return Page();
    }
}