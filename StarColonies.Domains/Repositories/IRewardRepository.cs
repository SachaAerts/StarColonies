using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Models.Missions;

namespace StarColonies.Domains.Repositories;

public interface IRewardRepository
{
    Task GiveRewardAsync(ColonistModel user, MissionResultModel result, int colonyId);
}