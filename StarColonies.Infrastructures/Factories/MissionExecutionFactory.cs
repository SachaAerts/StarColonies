using StarColonies.Infrastructures.Data.Entities.Missions;

namespace StarColonies.Infrastructures.Factories;

public class MissionExecutionFactory
{
    public static MissionExecutionEntity Create(int colonyId, int missionId, int planetId, bool livingColony,
        bool overcomingMission, bool isSuccess, int rewardedCoins) 
        => new()
            {
                ColonyId = colonyId,
                MissionId = missionId,
                PlanetId = planetId,
                LivingColony = livingColony,
                OvercomingMission = overcomingMission,
                IsSuccess = isSuccess,
                RewardedCoins = rewardedCoins
            };
}