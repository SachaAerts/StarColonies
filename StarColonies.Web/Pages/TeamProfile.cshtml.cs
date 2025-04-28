using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Repositories;
using StarColonies.Domains.Services.pictures;
using StarColonies.Infrastructures.Services.picture;

namespace StarColonies.Web.Pages;

[Authorize]
public class TeamProfile(IColonyRepository colonyRepository, IColonistRepository colonistRepository)
    : PageModel
{
    [BindProperty(SupportsGet = true)]
    public int TeamId { get; set; }
    
    public ColonistModel? TeamOwner { get; set; }
    
    public ColonyModel? Colony { get; set; }
    
    public async Task<IActionResult> OnGetAsync()
    {
        Colony = await colonyRepository.GetColonyByIdAsync(TeamId);
        TeamOwner = await colonistRepository.GetColonistByIdAsync(Colony!.OwnerId);
        return Page();
    }
    
    public async Task<IActionResult> OnPostDeleteAsync()
    {
        Colony = await colonyRepository.GetColonyByIdAsync(TeamId);
        TeamOwner = await colonistRepository.GetColonistByIdAsync(Colony!.OwnerId);

        IDeletePicture deletePicture = new DeletePicture();
        deletePicture.DeleteImage(Colony.LogoPath);
        
        await colonyRepository.DeleteColonyAsync(Colony.Id);
        
        return RedirectToPage("/Profile", new { id = TeamOwner!.Id });
    }
}