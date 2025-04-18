using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StarColonies.Infrastructures.Data;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Data.Entities.Missions;

namespace StarColonies.Web.Pages;

public class Map(StarColoniesDbContext? context, UserManager<ColonistEntity> userManager) : PageModel
{
    public IList<ColonieEntity> Colonies { get; set; } = new List<ColonieEntity>();
    public IList<PlanetEntity> Planets { get; set; } = new List<PlanetEntity>();

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            Console.WriteLine("User not found");
            return RedirectToPage("/Connection");
        }
        
        Planets = await context!.Planets
                .Include(p => p.Missions)
                .ThenInclude(m => m.Enemies)
                .Include(p => p.Missions)
                .ThenInclude(m => m.Rewards)
                .ThenInclude(r => r.Item)
                .ToListAsync();
        
        Colonies = await context.Colonies
            .Where(c => c.Members.Any(m => m.ColonistId == user.Id))
            .ToListAsync();

        return Page();
    }
    
}