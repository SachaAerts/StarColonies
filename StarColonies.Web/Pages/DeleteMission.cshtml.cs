using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Repositories;
using StarColonies.Web.Factories;

namespace StarColonies.Web.Pages;

[IgnoreAntiforgeryToken]
public class DeleteMission(
    IMissionRepository missionRepository,
    IResultFactory<JsonResult, object> jsonResult) : PageModel
{
    public async Task<IActionResult> OnPostAsync(int id)
    {
        Console.WriteLine($"NOT VISIBLE MISSION ID: {id}");
        await missionRepository.VisibleMissionAsync(id);
        return jsonResult.Create(true);
    }
}