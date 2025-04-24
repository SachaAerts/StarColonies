using StarColonies.Domains.Models.Missions;
using StarColonies.Infrastructures.Data.Entities;

namespace StarColonies.Infrastructures.Services.RewardStrategy;

public interface IMissionRewardStrategy
{
    Task ExecuteAsync(ColonistEntity user, MissionResultModel result, int colonyId);
}