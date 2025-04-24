using StarColonies.Domains.Models.Missions;

namespace StarColonies.Infrastructures.Services.RewardStrategy;

public interface IStrategyFactory
{
    IMissionRewardStrategy? GetStrategy(MissionResultModel result);
}