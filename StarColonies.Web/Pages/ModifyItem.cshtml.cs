using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Repositories;
using StarColonies.Domains.Services.pictures;
using StarColonies.Infrastructures.Services.picture;
using StarColonies.Web.wwwroot.models;

namespace StarColonies.Web.Pages;

public class ModifyItem(IItemRepository itemRepository, IWebHostEnvironment env)
    : PageModel
{
    [BindProperty(SupportsGet = true)]
    public int ItemId { get; set; }
    
    public ItemModel? Item { get; set; }
    
    [BindProperty]
    public NewItem ModifItem { get; set; }
    
    public async Task<IActionResult> OnGetAsync()
    {
        Item = await itemRepository.GetItemByIdAsync(ItemId);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if(!ModelState.IsValid)
            return Page();
        
        ItemModel? item = await itemRepository.GetItemByIdAsync(ItemId);
        string newPicture;
        if (ModifItem.Picture == item!.ImagePath)
        {
            newPicture = item.ImagePath;
        }
        else
        {
            var uploadPath = Path.Combine(env.WebRootPath, "img", "upload");   
            AnalyzeItemPicture analyzeItemPicture = new AnalyzeItemPicture(ModifItem.NameItem, uploadPath);
            
            IDeletePicture deletePicture = new DeletePicture();
            deletePicture.DeleteImage(item.ImagePath, true);
            newPicture = analyzeItemPicture.SaveItemPicture(ModifItem.Picture);
        }
        
        EffectModel newEffect = new EffectModel()
        {
            ForceModifier = ModifItem.ForceModifier,
            StaminaModifier = ModifItem.StaminaModifier,
        };
        
        var newItem = new ItemModel()
        {
            Id = ItemId,
            Name = ModifItem.NameItem,
            CoinsValue = ModifItem.Price,
            Description = "description",
            ImagePath = newPicture,
            Effect = newEffect,
            IsLegendary = ModifItem.IsLegendary,
        };

        await itemRepository.UpdateItemAsync(newItem, newEffect);
        
        return RedirectToPage("ItemsList");
    }
}