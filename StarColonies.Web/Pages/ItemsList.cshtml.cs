using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Repositories;
using StarColonies.Domains.Services.pictures;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Services.picture;

namespace StarColonies.Web.Pages;

[Authorize(Roles = "Admin")]
public class ItemsList(UserManager<ColonistEntity> userManager, IItemRepository itemRepository)
    : PageModel
{
    public IList<ItemModel> Items { get; set; } = new List<ItemModel>();
    
    public ColonistEntity? Admin { get; set; }
     
    public async Task<IActionResult> OnGetAsync()
    {
        Admin = await userManager.GetUserAsync(User);
        Items = await itemRepository.GetAllItemsAsync();
        return Page();
    }
    
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        ItemModel? item = await itemRepository.GetItemByIdAsync(id);

        IDeletePicture deletePicture = new DeletePicture();
        deletePicture.DeleteImage(item!.ImagePath, true);
        
        await itemRepository.DeleteItemAsync(id);
        
        return RedirectToPage(); 
    }
}