namespace StarColonies.Domains.Models.Missions;

public class MissionResultModel
{
    public bool OvercomingMission { get; set; }
    public bool LivingColony { get; set; }
    
    public bool MissionSuccess => OvercomingMission && LivingColony;
}