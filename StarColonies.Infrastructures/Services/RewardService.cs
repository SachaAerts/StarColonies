using Microsoft.AspNetCore.Identity;
using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Models.Missions;
using StarColonies.Domains.Repositories;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Mapper.DomainToEntity;

namespace StarColonies.Infrastructures.Services;

public class RewardService(
    UserManager<ColonistEntity> userManager, 
    IInventaryRepository inventaryRepository) : IRewardService
{
    public async Task GiveLevelsToMembersAsync(IList<ColonistModel> colonistModels)
    {
        foreach (var colonist in colonistModels)
        {
            var entity = await userManager.FindByIdAsync(colonist.Id);
            if (entity == null) continue;
            
            entity.Level = colonist.Level + 1;
            
            await userManager.UpdateAsync(entity);
        }
    }

    public async Task GiveResourcesToOwnerAsync(ColonistEntity user, MissionResultModel result)
    {
        user.Musty += result.CoinsReward;
        foreach (var reward in result.Rewards)
            await inventaryRepository.AddItemToUser(user.Id, reward);
    }
    
    public Task GiveMoneyToOwnerAsync(ColonistEntity user, MissionResultModel result)
    {
        user.Musty += result.CoinsReward;
        return Task.CompletedTask;
    }
}