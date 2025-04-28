using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Repositories;
using StarColonies.Infrastructures.Data.Entities;

namespace StarColonies.Web.Pages;

public class BlackmarketSelling(UserManager<ColonistEntity> userManager, IItemRepository itemRepository, IColonistRepository colonistRepository, IInventaryRepository inventoryRepository) : PageModel
{
    
    public required ColonistModel Colonist { get; set; }
    
    public required IList<RewardItemModel> InventoryItems { get; set; }
    
    public async Task<IActionResult> OnGetAsync()
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null) return RedirectToPage("/Connection");
        if (!User.Identity?.IsAuthenticated ?? true) return Forbid();
        
        Colonist = await colonistRepository.GetColonistByIdAsync(user.Id);
        
        InventoryItems = await inventoryRepository.GetItemsForColonistAsync(user.Id);
        return Page();
    }
    
    public async Task<IActionResult> OnPostSellItemAsync(int itemId, int itemValue)
    {
        var user = await userManager.GetUserAsync(User);
        
        var item = await itemRepository.GetItemByIdAsync(itemId);
        if (item == null) return NotFound();
        
        await inventoryRepository.SubstractItemToUserFromShop(user!.Id, item);
        await colonistRepository.AddMustyColonistAsync(user.Id, itemValue);
    
        return RedirectToPage();
    }
}