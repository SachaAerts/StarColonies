using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StarColonies.Infrastructures.Data;
using StarColonies.Infrastructures.Data.Entities.Missions;

namespace StarColonies.Web.Pages;

public class Map(StarColoniesDbContext context) : PageModel
{
    
    public IList<PlanetEntity> Planets { get; set; } = new List<PlanetEntity>();
    public IList<EnemyEntity> Enemies { get; set; } = new List<EnemyEntity>();
    
    public async Task OnGetAsync()
    {
        Planets = await context.Planets
            .Include(p => p.Missions)
            .ThenInclude(m => m.Enemies)
            .Include(p => p.Missions)
            .ThenInclude(m => m.Rewards)
            .ThenInclude(r => r.Item)
            .ToListAsync();
    }
    
}