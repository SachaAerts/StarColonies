using System.Collections;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StarColonies.Domains.Models;
using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Models.Missions;
using StarColonies.Domains.Repositories;
using StarColonies.Domains.Services;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Web.Factories;
using StarColonies.Web.Pages;

namespace StarColonies.Web.Services;

public class MissionExecutionService(
    MissionResolverService missionService,
    IPlanetRepository planetRepository,
    IInventaryRepository inventaryRepository,
    IColonyRepository colonyRepository,
    IRewardRepository rewardRepository,
    IColonistRepository colonistRepository,
    IMissionRepository missionRepository) : IMissionExecutionService
{
    private IList<ColonyModel> _allColonies = new List<ColonyModel>();
    private IList<RewardItemModel> _allItems = new List<RewardItemModel>();
    private IList<PlanetModel> _allPlanets = new List<PlanetModel>();
    private IList<ItemModel> _selectedItems = new List<ItemModel>();
    
    private MissionModel? _mission = new();
    private ColonyModel? _colony = new();
    
    
    public async Task<MissionExecutionResultModel> ResolveAndExecuteMissionAsync([FromBody] MissionRequestModel request, ColonistEntity user)
    {
        ArgumentNullException.ThrowIfNull(request);  ArgumentNullException.ThrowIfNull(user);

        await LoadUserContextAsync(user.Id, request);
        
        if (_mission == null || _colony == null) throw new InvalidOperationException("Invalid Mission or Colony");

        MissionResultModel result = missionService.Result(_mission, _colony, _selectedItems);
        ColonistModel colonist = await colonistRepository.GetColonistByIdAsync(user.Id);
        
        await Execute(colonist, result, _mission, _colony, _selectedItems);
        
        return new MissionExecutionResultModel { Result = result, Mission = _mission };
    }
    
    private async Task Execute(ColonistModel colonist, MissionResultModel result, MissionModel mission, ColonyModel colony, IList<ItemModel> selectedItems)
    {
        await rewardRepository.GiveRewardAsync(colonist, result, colony.Id);
        await inventaryRepository.UseItemFromUserAsync(colonist.Id, selectedItems);
        await missionRepository.MissionExecute(mission.Id, colony.Id, result);
    }

    private async Task LoadUserContextAsync(string userId, [FromBody] MissionRequestModel request)
    {
        _allColonies = await colonyRepository.GetColoniesForColonistAsync(userId);
        _allItems = await inventaryRepository.GetItemsForColonistAsync(userId);
        _allPlanets = await planetRepository.GetPlanetsWithMissionsAsync();
        _mission = _allPlanets.SelectMany(p => p.Missions).FirstOrDefault(m => m.Id == request.MissionId);
        _colony = _allColonies.FirstOrDefault(c => c.Id == request.ColonyId);
        _selectedItems = _allItems.Where(i => request.ItemIds.Contains(i.Item.Id)).Select(i => i.Item).ToList();
    }
}