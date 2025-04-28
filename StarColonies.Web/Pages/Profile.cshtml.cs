using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Repositories;
using StarColonies.Infrastructures.Data.Entities;

namespace StarColonies.Web.Pages;

[Authorize]
public class Profile(UserManager<ColonistEntity> userManager, IColonistRepository colonistRepository, IColonyRepository colonyRepository) 
    : PageModel
{
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }
    
    public required ColonistModel Colonist { get; set; }
    
    public required IList<ColonyModel> Colonies { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        if (!User.Identity?.IsAuthenticated ?? true)
            return Forbid();
        
        var user = await userManager.GetUserAsync(HttpContext.User);
        if (user!.Id != Id.ToString())
        {
            return RedirectToPage("Index");
        }
        
        Colonist = await colonistRepository.GetColonistByIdAsync(Id.ToString());
        Colonies = await colonyRepository.GetColoniesForColonistAsync(Colonist.Id);
        
        return Page();
    }
}