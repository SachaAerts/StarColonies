using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models;
using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Repositories;
using StarColonies.Domains.Services;
using StarColonies.Infrastructures.Data.Entities;

namespace StarColonies.Web.Pages;

public class Map(IMapRepository mapRepository, IInventaryRepository inventaryRepository, UserManager<ColonistEntity> userManager) : PageModel
{
    
    public IList<PlanetModel> Planets { get; private set; } = new List<PlanetModel>();
    public IList<ColonyModel> Colonies { get; private set; } = new List<ColonyModel>();
    public IList<ItemModel> Items { get; private set; } = new List<ItemModel>();
    
    public MissionResolverService MissionService => new();

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await userManager.GetUserAsync(User);
        
        if (user == null)
            return RedirectToPage("/Connection");

        Planets  = await mapRepository.GetPlanetsWithMissionsAsync();
        Items    = await inventaryRepository.GetItemsForColonistAsync(user.Id);
        Colonies = await mapRepository.GetColoniesForColonistAsync(user.Id);

        return Page();
    }
    
    public async Task<IActionResult> OnPostResolveAsync()
    {
        var user = await userManager.GetUserAsync(User);
        return new JsonResult(null);
    }
    
}