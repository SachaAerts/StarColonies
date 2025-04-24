using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models.Missions;
using StarColonies.Domains.Repositories;
using StarColonies.Web.Validators;

namespace StarColonies.Web.Pages;

public class ModifyMission(
    IMissionRepository missionRepository,
    IEnemyRepository enemyRepository
    ) : PageModel
{
    [BindProperty]
    public required MissionModel Mission { get; set; }
    
    [BindProperty]
    [MissionValidatorService(MaxEnemies = 3)]
    public List<int> SelectedEnemyIds { get; set; } = [];
    
    public IList<EnemyModel> Enemies { get; set; } = new List<EnemyModel>();
    
    public async Task<IActionResult> OnGetAsync(int id)
    {
        Mission = await missionRepository.GetMissionByIdAsync(id);
        Enemies = await enemyRepository.GetAllEnemiesListAsync();
        
        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        
        await missionRepository.UpdateMissionAsync(Mission, SelectedEnemyIds);
        return RedirectToPage("/Map");
    }
    
}