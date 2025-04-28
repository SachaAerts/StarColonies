using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Repositories;
using StarColonies.Domains.Services.pictures;
using StarColonies.Infrastructures.Repositories;
using StarColonies.Web.wwwroot.models;

namespace StarColonies.Web.Pages;

[Authorize(Roles = "Admin")]
public class CreateItem(IItemRepository itemRepository, IWebHostEnvironment env)
    : PageModel
{
    [BindProperty] 
    public NewItem NewItem { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var uploadPath = Path.Combine(env.WebRootPath, "img", "upload");
        AnalyzeItemPicture analyzeItemPicture = new AnalyzeItemPicture(NewItem.NameItem, uploadPath);

        EffectModel effect = new EffectModel()
        {
            ForceModifier = NewItem.ForceModifier,
            StaminaModifier = NewItem.StaminaModifier,
        };

        var item = new ItemModel()
        {
            Name = NewItem.NameItem,
            CoinsValue = NewItem.Price,
            Description = "description",
            ImagePath = analyzeItemPicture.SaveItemPicture(NewItem.Picture),
            Effect = effect,
            IsLegendary = NewItem.IsLegendary,
            NumberOfBuy = 0
        };
        
        await itemRepository.CreateItemAsync(item, effect);
        
        return RedirectToPage("ItemsList");
    }
}