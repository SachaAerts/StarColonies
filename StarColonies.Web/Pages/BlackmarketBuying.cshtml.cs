using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Repositories;
using StarColonies.Infrastructures.Data.Entities;

namespace StarColonies.Web.Pages;

[Authorize]
public class BlackmarketBuying(
    UserManager<ColonistEntity> userManager, 
    IColonistRepository colonistRepository, 
    IColonistFinanceRepository colonistFinanceRepository,
    IItemRepository itemRepository, IInventaryRepository inventoryRepository)
    : PageModel
{
    public required ColonistModel Colonist { get; set; }
    
    public required IList<ItemModel> StoreItems { get; set; }
    
    public required IList<RewardItemModel> InventoryItems { get; set; }
    
    public async Task<IActionResult> OnGetAsync()
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null) return RedirectToPage("/Connection");
        if (!User.Identity?.IsAuthenticated ?? true) return Forbid();
        
        Colonist = await colonistRepository.GetColonistByIdAsync(user.Id);
        StoreItems = await itemRepository.GetAllItemsAsync();
        
        var excludedNames = new[] { "AK-47", "Golden Apple", "Uncommon Artifact", "Golden Kebab" };
        StoreItems = StoreItems.Where(item => !excludedNames.Contains(item.Name)).ToList();
        
        InventoryItems = await inventoryRepository.GetItemsForColonistAsync(user.Id);
        return Page();
    }

    public async Task<IActionResult> OnPostPurchaseItemAsync(int itemId, int itemValue)
    {
        var user = await userManager.GetUserAsync(User);

        if (user!.Musty < itemValue) return RedirectToPage();
        
        var item = await itemRepository.GetItemByIdAsync(itemId);
        if (item == null)
            return NotFound();

        await inventoryRepository.AddItemToUserFromShop(user.Id, item);
        await colonistFinanceRepository.DebitColonistAsync(user.Id, itemValue);

        return RedirectToPage();
    }
}