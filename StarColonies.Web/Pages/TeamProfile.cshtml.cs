using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Repositories;
using StarColonies.Domains.Services.pictures;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Services.picture;

namespace StarColonies.Web.Pages;

[Authorize]
public class TeamProfile(UserManager<ColonistEntity> userManager, IColonyRepository colonyRepository, IColonistRepository colonistRepository)
    : PageModel
{
    [BindProperty(SupportsGet = true)]
    public int TeamId { get; set; }
    
    public ColonistModel? TeamOwner { get; set; }
    
    public ColonyModel? Colony { get; set; }
    
    public async Task<IActionResult> OnGetAsync()
    {
        var user = await userManager.GetUserAsync(HttpContext.User);
        Colony = await colonyRepository.GetColonyByIdAsync(TeamId);
        TeamOwner = await colonistRepository.GetColonistByIdAsync(Colony!.OwnerId);
        if (user!.Id != TeamOwner.Id)
        {
            return RedirectToPage("Index");
        }
        
        return Page();
    }
    
    public async Task<IActionResult> OnPostDeleteAsync()
    {
        Colony = await colonyRepository.GetColonyByIdAsync(TeamId);
        TeamOwner = await colonistRepository.GetColonistByIdAsync(Colony!.OwnerId);

        IDeletePicture deletePicture = new DeletePicture();
        deletePicture.DeleteImage(Colony.LogoPath, false);
        
        await colonyRepository.DeleteColonyAsync(Colony.Id);
        
        return RedirectToPage("/Profile", new { id = TeamOwner!.Id });
    }
}