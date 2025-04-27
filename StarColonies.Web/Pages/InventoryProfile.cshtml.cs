using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Repositories;

namespace StarColonies.Web.Pages;

public class InventoryProfile(IInventaryRepository inventoryRepository)
    : PageModel
{
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    public string[] ItemPictureList { get; set; }
    
    public IList<RewardItemModel> Inventory { get; set; } = new List<RewardItemModel>();
    
    public async Task<IActionResult> OnGet()
    {
        Inventory = await inventoryRepository.GetItemsForColonistAsync(Id.ToString());
        
        return Page();
    }
}