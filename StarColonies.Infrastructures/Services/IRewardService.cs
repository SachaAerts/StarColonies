using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Models.Missions;
using StarColonies.Infrastructures.Data.Entities;

namespace StarColonies.Infrastructures.Services;

public interface IRewardService
{
    Task GiveLevelsToMembersAsync(IList<ColonistModel> colonistModels);
    Task GiveResourcesToOwnerAsync(ColonistEntity user, MissionResultModel result);
    Task GiveMoneyToOwnerAsync(ColonistEntity user, MissionResultModel result);
}