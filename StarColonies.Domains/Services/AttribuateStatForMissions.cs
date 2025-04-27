using StarColonies.Domains.Models;
using StarColonies.Domains.Models.Missions;

namespace StarColonies.Domains.Services;

public class AttribuateStatForMissions
{
    public int[] MissionSucceed { get; set; }
    
    public int[] MissionFailed { get; set; }
    
    public AttribuateStatForMissions(IList<PlanetModel> planets, IList<MissionExecutedModel> missionExecutedList)
    {
        MissionSucceed = new int[planets.Count];
        MissionFailed = new int[planets.Count];
        AttribuateStatistics(planets, missionExecutedList);
    }

    private void AttribuateStatistics(IList<PlanetModel> planets, IList<MissionExecutedModel> missionExecutedList)
    {
        foreach (MissionExecutedModel missionExecuted in missionExecutedList)
        {
            if (missionExecuted.IsSuccess)
            {
                MissionSucceed[missionExecuted.PlanetId - 1]++;
            }
            else
            {
                MissionFailed[missionExecuted.PlanetId - 1]++;
            }
        }
    }
}