using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models;
using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Models.Items;
using StarColonies.Web.Factories;
using StarColonies.Web.Services;

namespace StarColonies.Web.Pages;

public class Map(
    IMissionExecutionService missionExecutionService,
    IMapDataService mapDataService,
    IResultFactory<JsonResult, object> jsonResultFactory) : PageModel
{
    public IList<PlanetModel> Planets { get; private set; } = new List<PlanetModel>();
    public IList<ColonyModel> Colonies { get; private set; } = new List<ColonyModel>();
    public IList<RewardItemModel> Inventory { get; private set; } = new List<RewardItemModel>();
    public List<ItemModel?> ItemsInventory { get; private set; } = [];
    public ColonistModel? MyColonist { get; private set; }
    
    public async Task<IActionResult> OnGetAsync()
    {
        MyColonist = await mapDataService.GetColonistAsync(User);
        if (MyColonist == null) return RedirectToPage("/Connection");

        Planets = await mapDataService.GetPlanetsWithMissionsAsync();
        Colonies = await mapDataService.GetColoniesForColonistAsync(MyColonist.Id);
        Inventory = await mapDataService.GetInventoryForColonistAsync(MyColonist.Id);
        ItemsInventory = await mapDataService.GetItemsFromInventoryAsync(MyColonist.Id);
        
        await mapDataService.AllocateRewardsToMissionsAsync(Planets);
        return Page();
    }
    
    public async Task<JsonResult> OnPostResolveMissionAsync([FromBody] MissionRequestModel request)
    {
        try
        {
            var result = await missionExecutionService.ResolveAndExecuteMissionAsync(User, request);
            var mission = Planets.SelectMany(p => p.Missions).FirstOrDefault(m => m.Id == request.MissionId);
            return jsonResultFactory.Create(true, new { result, mission });
        } catch (Exception ex) { return jsonResultFactory.Create(false, ex.Message); }
    }
    
}

[BindProperties]
public class MissionRequestModel
{
    public int MissionId { get; set; }
    public int ColonyId { get; set; }
    public List<int> ItemIds { get; set; } = new();
}
 