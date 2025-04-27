using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using StarColonies.Domains.Models;
using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Repositories;
using StarColonies.Infrastructures.Data.Entities;

namespace StarColonies.Web.Services;

public class MapDataService(
    IPlanetRepository planetRepository,
    IInventaryRepository inventaryRepository,
    IColonyRepository colonyRepository,
    IColonistRepository colonistRepository,
    IRewardRepository rewardRepository,
    UserManager<ColonistEntity> userManager) : IMapDataService
{
    public async Task<ColonistModel?> GetColonistAsync(ClaimsPrincipal user)
    {
        var entity = await userManager.GetUserAsync(user);
        return entity == null ? null : await colonistRepository.GetColonistByIdAsync(entity.Id);
    }

    public async Task<IList<PlanetModel>> GetPlanetsWithMissionsAsync()
        => await planetRepository.GetPlanetsWithMissionsAsync();

    public async Task<IList<ColonyModel>> GetColoniesForColonistAsync(string colonistId)
        => await colonyRepository.GetColoniesForColonistAsync(colonistId);

    public async Task<IList<RewardItemModel>> GetInventoryForColonistAsync(string colonistId)
        => await inventaryRepository.GetItemsForColonistAsync(colonistId);

    public async Task<List<ItemModel?>> GetItemsFromInventoryAsync(string colonistId)
        => (await GetInventoryForColonistAsync(colonistId)).Select(i => i.Item).ToList();

    public async Task AllocateRewardsToMissionsAsync(IList<PlanetModel> planets)
    {
        foreach (var mission in planets.SelectMany(p => p.Missions))
            mission.Items = await rewardRepository.GetRewardsForMissionAsync(mission.Id);
    }
}