using Microsoft.AspNetCore.Identity;
using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Models.Missions;
using StarColonies.Domains.Repositories;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Mapper.DomainToEntity;

namespace StarColonies.Infrastructures.Services;

public class RewardService(
    UserManager<ColonistEntity> userManager, 
    IInventaryRepository inventaryRepository,
    IDomainToEntityMapper<ColonistEntity, ColonistModel> mapper)
{
    public async Task GiveLevelsToMembersAsync(IList<ColonistModel> colonistModels)
    {
        var updateTasks = colonistModels.Select(async colonist =>
        {
            colonist.Level += 1;
            var entity = mapper.Map(colonist);
            await userManager.UpdateAsync(entity);
        });

        await Task.WhenAll(updateTasks);
    }

    public async Task GiveResourcesToOwnerAsync(ColonistEntity user, MissionResultModel result)
    {
        user.Level += 1;
        user.Musty += result.CoinsReward;

        var itemAddTasks = result.Rewards.Select(r => inventaryRepository.AddItemToUser(user.Id, r));
        await Task.WhenAll(itemAddTasks);
    }
}