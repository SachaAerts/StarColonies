using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using StarColonies.Domains.Models.Missions;
using StarColonies.Domains.Repositories;
using StarColonies.Domains.Services;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Web.Pages;

namespace StarColonies.Web.Services;

public class MissionExecutionService(
    IPlanetRepository planetRepository,
    IInventaryRepository inventaryRepository,
    IColonyRepository colonyRepository,
    IColonistRepository colonistRepository,
    IMissionRepository missionRepository,
    IRewardRepository rewardRepository,
    UserManager<ColonistEntity> userManager) : IMissionExecutionService
{
    private readonly MissionResolverService _resolver = new();

    public async Task<MissionResultModel> ResolveAndExecuteMissionAsync(ClaimsPrincipal user, MissionRequestModel request)
    {
        var entity = await userManager.GetUserAsync(user);
        if (entity == null) throw new InvalidOperationException("User not authenticated");

        var colonistId = entity.Id;
        var allColonies = await colonyRepository.GetColoniesForColonistAsync(colonistId);
        var allItems = await inventaryRepository.GetItemsForColonistAsync(colonistId);
        var allPlanets = await planetRepository.GetPlanetsWithMissionsAsync();

        var mission = allPlanets.SelectMany(p => p.Missions).FirstOrDefault(m => m.Id == request.MissionId);
        var colony = allColonies.FirstOrDefault(c => c.Id == request.ColonyId);
        var selectedItems = allItems.Where(i => i.Item != null && request.ItemIds.Contains(i.Item.Id)).Select(i => i.Item).ToList();

        if (mission == null || colony == null) throw new InvalidOperationException("Invalid Mission or Colony");

        var result = _resolver.Result(mission, colony, selectedItems);
        var colonist = await colonistRepository.GetColonistByIdAsync(colonistId);

        await rewardRepository.GiveRewardAsync(colonist, result, colony.Id);
        await inventaryRepository.UseItemFromUserAsync(colonistId, selectedItems);
        await missionRepository.MissionExecute(mission.Id, colony.Id, result);

        return result;
    }
}