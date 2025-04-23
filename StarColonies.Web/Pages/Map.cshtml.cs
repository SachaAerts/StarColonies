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

namespace StarColonies.Web.Pages;

public class Map(
    IMapRepository mapRepository, 
    IInventaryRepository inventaryRepository,
    IColonyRepository colonyRepository,
    IColonistRepository colonistRepository,
    IRewardRepository rewardRepository,
    IResultFactory<JsonResult, object> jsonResultFactory,
    UserManager<ColonistEntity> userManager) : PageModel
{
    public IList<PlanetModel> Planets { get; private set; } = new List<PlanetModel>();
    public IList<ColonyModel> Colonies { get; private set; } = new List<ColonyModel>();
    public IList<ItemModel> Items { get; private set; } = new List<ItemModel>();
    
    public MissionResolverService MissionService => new();

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await userManager.GetUserAsync(User);
        
        if (user == null) return RedirectToPage("/Connection");

        Planets  = await mapRepository.GetPlanetsWithMissionsAsync();
        Items    = await inventaryRepository.GetItemsForColonistAsync(user.Id);
        Colonies = await colonyRepository.GetColoniesForColonistAsync(user.Id);
        
        await AllocateRewardsToMissionAsync();
        
        return Page();
    }
    
    public async Task<JsonResult> OnPostResolveMissionAsync([FromBody] MissionRequestModel request)
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null) return jsonResultFactory.Create(false, "Utilisateur non connecté");
        
        var allColonies = await colonyRepository.GetColoniesForColonistAsync(user.Id);
        var allItems = await inventaryRepository.GetItemsForColonistAsync(user.Id);
        var allPlanets = await mapRepository.GetPlanetsWithMissionsAsync();

        var mission = allPlanets.SelectMany(p => p.Missions).FirstOrDefault(m => m.Id == request.MissionId);
        var colony = allColonies.FirstOrDefault(c => c.Id == request.ColonyId);
        var selectedItems = allItems.Where(i => request.ItemIds.Contains(i.Id)).ToList();

        if (mission == null || colony == null) 
            return jsonResultFactory.Create(false, "Paramètres invalides [mission ou colonie]");

        MissionResultModel result = IsMissionResolved(mission, colony, selectedItems);
        
        var colonist = await colonistRepository.GetColonistByIdAsync(user.Id);
        
        //await rewardRepository.GiveRewardAsync(colonist, result, colony.Id);
        return jsonResultFactory.Create(true, new {result, mission});
    }

    private MissionResultModel IsMissionResolved(MissionModel mission, ColonyModel colony, IList<ItemModel> items) 
        => MissionService.Result(mission, colony, items);

    private async Task AllocateRewardsToMissionAsync()
    {
        foreach (var mission in Planets.SelectMany(p => p.Missions))
            mission.Items = await rewardRepository.GetRewardsForMissionAsync(mission.Id);
    }
    
}

[BindProperties]
public class MissionRequestModel
{
    public int MissionId { get; set; }
    public int ColonyId { get; set; }
    public List<int> ItemIds { get; set; } = new();
}
 