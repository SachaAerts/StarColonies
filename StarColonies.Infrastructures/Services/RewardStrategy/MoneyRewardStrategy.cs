using StarColonies.Domains.Models.Missions;
using StarColonies.Infrastructures.Data.Entities;

namespace StarColonies.Infrastructures.Services.RewardStrategy;

public class MoneyRewardStrategy(IRewardService rewardService) : IMissionRewardStrategy
{
    public async Task ExecuteAsync(ColonistEntity user, MissionResultModel result, int colonyId)
        => await rewardService.GiveMoneyToOwnerAsync(user, result);
}