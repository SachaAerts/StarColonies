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
    IPlanetRepository planetRepository, 
    IInventaryRepository inventaryRepository,
    IColonyRepository colonyRepository,
    IColonistRepository colonistRepository,
    IMissionRepository missionRepository,
    IRewardRepository rewardRepository,
    IResultFactory<JsonResult, object> jsonResultFactory,
    UserManager<ColonistEntity> userManager) : PageModel
{
    public IList<PlanetModel> Planets { get; private set; } = new List<PlanetModel>();
    public IList<ColonyModel> Colonies { get; private set; } = new List<ColonyModel>();
    public IList<RewardItemModel> Inventory { get; private set; } = new List<RewardItemModel>();
    public IList<ItemModel> ItemsInventory { get; private set; } = new List<ItemModel>();
    
    public ColonistModel? MyColonist { get; private set; }
    
    public MissionResolverService MissionService => new();

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null) return RedirectToPage("/Connection");
        
        MyColonist = await colonistRepository.GetColonistByIdAsync(user.Id);
        Planets   = await planetRepository.GetPlanetsWithMissionsAsync();
        Inventory = await inventaryRepository.GetItemsForColonistAsync(user.Id);
        ItemsInventory = Inventory.Select(i => i.Item).ToList();
        Colonies  = await colonyRepository.GetColoniesForColonistAsync(user.Id);
        
        await AllocateRewardsToMissionAsync();
        
        return Page();
    }
    
    public async Task<JsonResult> OnPostResolveMissionAsync([FromBody] MissionRequestModel request)
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null) return JsonResultError("User not found");
        
        IList<ColonyModel> allColonies = await colonyRepository.GetColoniesForColonistAsync(user.Id);
        IList<RewardItemModel> allItems = await inventaryRepository.GetItemsForColonistAsync(user.Id);
        IList<PlanetModel> allPlanets = await planetRepository.GetPlanetsWithMissionsAsync();

        MissionModel? mission = allPlanets.SelectMany(p => p.Missions).FirstOrDefault(m => m.Id == request.MissionId);
        ColonyModel? colony = allColonies.FirstOrDefault(c => c.Id == request.ColonyId);
        IList<ItemModel> selectedItems = allItems.Where(i => request.ItemIds.Contains(i.Item.Id)).Select(i => i.Item).ToList();
        
        if (mission == null || colony == null) return JsonResultError("Parameters are not valid [Mission or Colony]");

        MissionResultModel result = IsMissionResolved(mission, colony, selectedItems);
        ColonistModel colonist = await colonistRepository.GetColonistByIdAsync(user.Id);
        
        await Execute(user.Id, colonist, result, mission, colony, selectedItems);
        return jsonResultFactory.Create(true, new {result, mission});
    }

    private async Task Execute(string guid, ColonistModel colonist, MissionResultModel result, MissionModel mission, ColonyModel colony, IList<ItemModel> selectedItems)
    {
        await rewardRepository.GiveRewardAsync(colonist, result, colony.Id);
        await inventaryRepository.UseItemFromUserAsync(guid, selectedItems);
        await missionRepository.MissionExecute(mission.Id, colony.Id, result);
    }

    private MissionResultModel IsMissionResolved(MissionModel mission, ColonyModel colony, IList<ItemModel> items) 
        => MissionService.Result(mission, colony, items);

    private async Task AllocateRewardsToMissionAsync()
    {
        foreach (var mission in Planets.SelectMany(p => p.Missions))
            mission.Items = await rewardRepository.GetRewardsForMissionAsync(mission.Id);
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
 