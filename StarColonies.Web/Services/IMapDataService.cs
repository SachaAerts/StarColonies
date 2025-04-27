using System.Security.Claims;
using StarColonies.Domains.Models;
using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Models.Items;

namespace StarColonies.Web.Services;

public interface IMapDataService
{
    Task<ColonistModel?> GetColonistAsync(ClaimsPrincipal user);
    Task<IList<PlanetModel>> GetPlanetsWithMissionsAsync();
    Task<IList<ColonyModel>> GetColoniesForColonistAsync(string colonistId);
    Task<IList<RewardItemModel>> GetInventoryForColonistAsync(string colonistId);
    Task<List<ItemModel?>> GetItemsFromInventoryAsync(string colonistId);
    Task AllocateRewardsToMissionsAsync(IList<PlanetModel> planets);
}