using StarColonies.Domains.Models.Missions;

namespace StarColonies.Web.Factories;

public class JsonContentMissionFactory : IJsonContentFactory
{
    public object Create(bool success, object? payload = null)
        => payload switch
        {
            string msg => new { success, message = msg },
            MissionPayload data => new { success, result = Build(data.Result, data.Mission) },
            _ => new { success, result = payload }
        };

    private object Build(MissionResultModel result, MissionModel mission)
        => new
        {
            isSuccess = result.MissionSuccess,
            livingColony = result.LivingColony,
            overcomingMission = result.OvercomingMission,
            description = result.ResultMessage,
            rewards = mission.Items.Select(i => new { i.Id, i.Name, i.Description, i.ImagePath }),
            coinsReward = mission.CoinsReward
        };
}

public class MissionPayload(MissionResultModel result, MissionModel mission)
{
    public MissionResultModel Result { get; set; } = result;
    public MissionModel Mission { get; set; } = mission;
}
