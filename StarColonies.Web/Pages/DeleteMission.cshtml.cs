using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Repositories;
using StarColonies.Web.Factories;

namespace StarColonies.Web.Pages;

public class DeleteMission(
    IMissionRepository missionRepository,
    IResultFactory<JsonResult, object> jsonResult) : PageModel
{
    public async Task<IActionResult> OnGetAsync(int id)
    {
        await missionRepository.DeleteMissionAsync(id);
        return jsonResult.Create(true);
    }
}