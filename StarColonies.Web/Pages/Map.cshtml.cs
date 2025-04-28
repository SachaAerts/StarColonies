using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models;
using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Models.Missions;
using StarColonies.Domains.Repositories;
using StarColonies.Domains.Services;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Web.Factories;
using StarColonies.Web.Services;

namespace StarColonies.Web.Pages;

public class Map(
    IPlanetRepository planetRepository, 
    IInventaryRepository inventaryRepository,
    IColonyRepository colonyRepository,
    IColonistRepository colonistRepository,
    IRewardRepository rewardRepository,
    IMissionExecutionService missionExecutionService,
    IResultFactory<JsonResult, object> jsonResultFactory,
    UserManager<ColonistEntity> userManager) : PageModel
{
    public IList<PlanetModel> Planets { get; private set; } = new List<PlanetModel>();
    public IList<ColonyModel> Colonies { get; private set; } = new List<ColonyModel>();
    public IList<RewardItemModel> Inventory { get; private set; } = new List<RewardItemModel>();
    public List<ItemModel?> ItemsInventory { get; private set; } = new();
    
    public ColonistModel? MyColonist { get; private set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null) return RedirectToPage("/Connection");
        
        await LoadUserContextAsync(user.Id);
        await AllocateRewardsToMissionAsync();
        
        return Page();
    }

    public async Task<JsonResult> OnPostResolveMissionAsync([FromBody] MissionRequestModel request)
    {
        var user = await userManager.GetUserAsync(User);
        try
        {
            MissionExecutionResultModel result = await missionExecutionService.ResolveAndExecuteMissionAsync(request, user!);
            return jsonResultFactory.Create(true, new {result.Result, result.Mission});
        } catch (Exception e) { return JsonResultError(e.Message); }
    }
    
    private async Task AllocateRewardsToMissionAsync()
    {
        foreach (var mission in Planets.SelectMany(p => p.Missions))
            mission.Items = await rewardRepository.GetRewardsForMissionAsync(mission.Id);
    }
    
    private async Task LoadUserContextAsync(string userId)
    {
        MyColonist = await colonistRepository.GetColonistByIdAsync(userId);
        Planets   = await planetRepository.GetPlanetsWithMissionsAsync();
        Inventory = await inventaryRepository.GetItemsForColonistAsync(userId);
        ItemsInventory = Inventory.Select(i => i.Item).ToList();
        Colonies  = await colonyRepository.GetColoniesForColonistAsync(userId);
    }
    
    private JsonResult JsonResultError(string message)
        => jsonResultFactory.Create(false, message);
}

[BindProperties]
public class MissionRequestModel
{
    public int MissionId { get; set; }
    public int ColonyId { get; set; }
    public List<int> ItemIds { get; set; } = new();
}
 