using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StarColonies.Infrastructures.Data;
using StarColonies.Infrastructures.Data.Entities.Missions;

namespace StarColonies.Web.Pages;

public class Map(StarColoniesDbContext context) : PageModel
{
    
    public List<PlanetEntity> Planets { get; set; } = new();
    
    public async void OnGet()
    {
        Planets = await context.Planets
            .Include(p => p.Missions)
            .ToListAsync();
    }
}