using StarColonies.Domains.Models.Missions;
using StarColonies.Infrastructures.Data.Entities;

namespace StarColonies.Infrastructures.Services.RewardStrategy;

public class ResourceRewardStrategy(IRewardService rewardService) : IMissionRewardStrategy
{
    public async Task ExecuteAsync(ColonistEntity user, MissionResultModel result, int colonyId)
        => await rewardService.GiveResourcesToOwnerAsync(user, result);
}