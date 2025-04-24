using StarColonies.Domains.Models.Missions;
using StarColonies.Infrastructures.Data.Entities;

namespace StarColonies.Infrastructures.Services.RewardStrategy;

public class NoRewardStrategy : IMissionRewardStrategy
{
    public Task ExecuteAsync(ColonistEntity user, MissionResultModel result, int colonyId)
        => Task.CompletedTask;
}