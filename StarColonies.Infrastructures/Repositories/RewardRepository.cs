using Microsoft.AspNetCore.Identity;
using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Models.Missions;
using StarColonies.Domains.Repositories;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Mapper.DomainToEntity;

namespace StarColonies.Infrastructures.Repositories;

public class RewardRepository(
    UserManager<ColonistEntity> userManager, 
    IInventaryRepository inventaryRepository,
    IDomainToEntityMapper<ColonistEntity, ColonistModel> mapper,
    IColonyRepository colonyRepository) : IRewardRepository
{
    public async Task GiveRewardAsync(ColonistModel userModel, MissionResultModel result, int colonyId)
    {
        ColonistEntity user = mapper.Map(userModel);
    
        if (result is { OvercomingMission: true, LivingColony: true })
        {
            var members = await colonyRepository.GetColonistsForColonyAsync(colonyId);
            await GiveLevelsToMembersAsync(members);

            await GiveResourcesToOwnerAsync(user, result);
            await userManager.UpdateAsync(user);
        }
    }

    private async Task GiveLevelsToMembersAsync(IList<ColonistModel> colonistModels)
    {
        var updateTasks = colonistModels.Select(async colonist =>
        {
            colonist.Level += 1;
            var entity = mapper.Map(colonist);
            await userManager.UpdateAsync(entity);
        });

        await Task.WhenAll(updateTasks);
    }

    private async Task GiveResourcesToOwnerAsync(ColonistEntity user, MissionResultModel result)
    {
        user.Level += 1;
        user.Musty += result.CoinsReward;

        var itemAddTasks = result.Rewards.Select(r => inventaryRepository.AddItemToUser(user.Id, r));
        await Task.WhenAll(itemAddTasks);
    }
    
}
