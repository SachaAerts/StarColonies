using StarColonies.Domains.Models.Missions;
using StarColonies.Domains.Repositories;
using StarColonies.Infrastructures.Data.Entities;

namespace StarColonies.Infrastructures.Services.RewardStrategy;

public class FullSuccessRewardStrategy(IColonyRepository colonyRepository, IRewardService rewardService) : IMissionRewardStrategy
{
    public async Task ExecuteAsync(ColonistEntity user, MissionResultModel result, int colonyId)
    {
        var members = await colonyRepository.GetColonistsForColonyAsync(colonyId);
        await rewardService.GiveLevelsToMembersAsync(members);
        await rewardService.GiveMoneyToOwnerAsync(user, result);
        await rewardService.GiveResourcesToOwnerAsync(user, result);
    }
}