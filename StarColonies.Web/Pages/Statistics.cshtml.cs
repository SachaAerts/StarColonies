using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models;
using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Repositories;
using StarColonies.Web.wwwroot.models;

namespace StarColonies.Web.Pages;

public class Statistics(IItemRepository itemRepository, IColonyRepository colonyRepository, IPlanetRepository planetRepository)
    : PageModel
{
    public StatisticModel Statistic { get; set; } = new();
    
    public async Task<IActionResult> OnGet()
    {
        IList<ItemModel> items = await itemRepository.GetAllItemsAsync();
        IList<ColonyModel> top10Colony = await colonyRepository.GetTop10ColoniesAsync();
        IList<PlanetModel> planetsList = await planetRepository.GetPlanetsWithMissionsAsync();
        
        FillStatistics(items, top10Colony);
        
        return Page();
    }

    private void FillStatistics(IList<ItemModel> items, IList<ColonyModel> top10Colony)
    {
        IList<string> itemsLabel = new List<string>();
        IList<int> numberOfBuysPerItems = new List<int>();
        IList<string> top10ColonyLabel = new List<string>();
        IList<int> top10ColonyStrength = new List<int>();
        IList<int> top10ColonyStamina = new List<int>();

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
        
        Statistic.ItemsLabel = itemsLabel;
        Statistic.NumberOfBuysPerItems = numberOfBuysPerItems;
        Statistic.Top10ColonyLabels = top10ColonyLabel;
        Statistic.Top10ColonyStrength = top10ColonyStrength;
        Statistic.Top10ColonyStamina = top10ColonyStamina;
    }
}