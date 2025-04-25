using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models.Colony;
using StarColonies.Infrastructures.Repositories;

namespace StarColonies.Web.Pages;

public class TeamProfile(ColonistRepository colonistRepository, ColonyRepository colonyRepository)
    : PageModel
{
    [BindProperty(SupportsGet = true)]
    public int TeamId { get; set; }
    
    public ColonistModel TeamOwner { get; set; }
    
    public ColonyModel Colony { get; set; }
    
    public void OnGet()
    {
        
    }
}