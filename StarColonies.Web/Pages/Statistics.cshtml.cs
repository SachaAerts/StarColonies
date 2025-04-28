using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models;
using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Models.Missions;
using StarColonies.Domains.Repositories;
using StarColonies.Domains.Services;
using StarColonies.Web.wwwroot.models;

namespace StarColonies.Web.Pages;

public class Statistics(IItemRepository itemRepository, IColonyRepository colonyRepository, IPlanetRepository planetRepository, IMissionRepository missionRepository)
    : PageModel
{
    public StatisticModel Statistic { get; set; } = new();
    
    public async Task<IActionResult> OnGetAsync()
    {
        IList<ItemModel> items = await itemRepository.GetAllItemsAsync();
        IList<ColonyModel> top10Colony = await colonyRepository.GetTop10ColoniesAsync();
        IList<PlanetModel> planetsList = await planetRepository.GetPlanetsWithMissionsAsync();
        IList<MissionExecutedModel> missionExecutedList = await missionRepository.GetAllMissionExecutionsAsync();
        
        
        FillStatsForFirstGraph(top10Colony);
        FillStatsForSecondGraph(items);
        FillStatsForThirdGraph(planetsList, missionExecutedList);
        
        return Page();
    }

    private void FillStatsForSecondGraph(IList<ItemModel> items)
    {
        IList<string> itemsLabel = new List<string>();
        IList<int> numberOfBuysPerItems = new List<int>();
        
        foreach (ItemModel item in items)
        {
            if (item.Name != "Uncommon Artifact" && item.Name != "Golden Apple" && item.Name != "AK-47")
            {
                itemsLabel.Add(item.Name);
                numberOfBuysPerItems.Add(item.NumberOfBuy);    
            }
        }
        
        Statistic.ItemsLabel = itemsLabel;
        Statistic.NumberOfBuysPerItems = numberOfBuysPerItems;
    }

    private void FillStatsForFirstGraph(IList<ColonyModel> top10Colony)
    {
        IList<string> top10ColonyLabel = new List<string>();
        IList<int> top10ColonyStrength = new List<int>();
        IList<int> top10ColonyStamina = new List<int>();
        
        foreach (ColonyModel colony in top10Colony)
        {
            top10ColonyLabel.Add(colony.Name);
            top10ColonyStrength.Add(colony.Strength);
            top10ColonyStamina.Add(colony.Stamina);
        }
        
        Statistic.Top10ColonyLabels = top10ColonyLabel;
        Statistic.Top10ColonyStrength = top10ColonyStrength;
        Statistic.Top10ColonyStamina = top10ColonyStamina;
    }

    private void FillStatsForThirdGraph(IList<PlanetModel> planetsList, IList<MissionExecutedModel> missionExecutedList)
    {
        IList<string> planetsLabel = new List<string>();
        
        foreach (PlanetModel planet in planetsList)
        {
            planetsLabel.Add(planet.Name);
        }
        
        Statistic.PlanetsLabel = planetsLabel;
        
        AttribuateStatForMissions attribuateStatForMissions = new AttribuateStatForMissions(planetsList, missionExecutedList);
        Statistic.MissionsSucceed = attribuateStatForMissions.MissionSucceed;
        Statistic.MissionsFailed = attribuateStatForMissions.MissionFailed;
    }
}