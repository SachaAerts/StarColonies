using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Repositories;

namespace StarColonies.Web.Pages;

public class Profile(IColonistRepository colonistRepository, IColonyRepository colonyRepository) 
    : PageModel
{
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }
    
    public required ColonistModel Colonist { get; set; }
    
    public required int LvlToAttribuate { get; set; }
    
    public required IList<ColonyModel> Colonies { get; set; }

    public async Task<IActionResult> OnGet()
    {
        if (!User.Identity?.IsAuthenticated ?? true)
            return Forbid();
        
        string id = Id.ToString();
        Console.WriteLine("test: " + id);
        Colonist = await colonistRepository.GetColonistByIdAsync(Id.ToString());
        Colonies = await colonyRepository.GetColoniesForColonistAsync(Colonist.Id);
        CalculateLvlToAttribuate();
        
        return Page();
    }

    private void CalculateLvlToAttribuate()
    {
        int maxPoint = 7 + (Colonist.Level - 1);
        LvlToAttribuate = maxPoint - Colonist.Strength - Colonist.Stamina;
    }
}