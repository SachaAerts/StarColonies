using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Repositories;

namespace StarColonies.Web.Pages;

public class DeleteMission(IMissionRepository missionRepository) : PageModel
{
    public async Task<IActionResult> OnPostAsync(int id)
    {
        await missionRepository.DeleteMissionAsync(id);
        return new JsonResult(new { success = true });
    }
}