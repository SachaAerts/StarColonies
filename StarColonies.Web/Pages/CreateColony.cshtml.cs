using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Repositories;
using StarColonies.Infrastructures.Data.Entities;

namespace StarColonies.Web.Pages;

public class CreateColony(IColonistRepository colonistRepository)
    : PageModel
{
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }
    
    [BindProperty]
    public IList<ColonistModel> Colonists { get; set; }
    
    public async Task<IActionResult> OnGet()
    {
        Colonists = await colonistRepository.GetColonistsAsync();
        return Page();
    }
}