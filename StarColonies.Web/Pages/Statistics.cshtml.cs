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
    
    public async Task<IActionResult> OnGet()
    {
        IList<ItemModel> items = await itemRepository.GetAllItemsAsync();
        IList<ColonyModel> top10Colony = await colonyRepository.GetTop10ColoniesAsync();
        IList<PlanetModel> planetsList = await planetRepository.GetPlanetsWithMissionsAsync();
        IList<MissionExecutedModel> missionExecutedList = await missionRepository.GetAllMissionExecutionsAsync();
        
        FillStatistics(items, top10Colony, planetsList);
        AttribuateStatForMissions attribuateStatForMissions = new AttribuateStatForMissions(planetsList, missionExecutedList);
        Statistic.MissionsSucceed = attribuateStatForMissions.MissionSucceed;
        Statistic.MissionsFailed = attribuateStatForMissions.MissionFailed;
        
        return Page();
    }

    private void FillStatistics(IList<ItemModel> items, IList<ColonyModel> top10Colony, IList<PlanetModel> planetsList)
    {
        IList<string> itemsLabel = new List<string>();
        IList<int> numberOfBuysPerItems = new List<int>();
        IList<string> top10ColonyLabel = new List<string>();
        IList<int> top10ColonyStrength = new List<int>();
        IList<int> top10ColonyStamina = new List<int>();
        IList<string> planetsLabel = new List<string>();

        foreach (ItemModel item in items)
        {
            itemsLabel.Add(item.Name);
            numberOfBuysPerItems.Add(item.NumberOfBuy);
        }

        foreach (ColonyModel colony in top10Colony)
        {
            top10ColonyLabel.Add(colony.Name);
            top10ColonyStrength.Add(colony.Strength);
            top10ColonyStamina.Add(colony.Stamina);
        }

        foreach (PlanetModel planet in planetsList)
        {
            planetsLabel.Add(planet.Name);
        }
        
        Statistic.ItemsLabel = itemsLabel;
        Statistic.NumberOfBuysPerItems = numberOfBuysPerItems;
        Statistic.Top10ColonyLabels = top10ColonyLabel;
        Statistic.Top10ColonyStrength = top10ColonyStrength;
        Statistic.Top10ColonyStamina = top10ColonyStamina;
        Statistic.PlanetsLabel = planetsLabel;
    }
}