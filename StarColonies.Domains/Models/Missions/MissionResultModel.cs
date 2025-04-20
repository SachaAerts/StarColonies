namespace StarColonies.Domains.Models.Missions;

public class MissionResultModel
{
    public bool OvercomingMission { get; set; }
    public bool LivingColony { get; set; }
    
    public bool MissionSuccess => OvercomingMission && LivingColony;
    
    public string ResultMessage
    {
        get => MissionSuccess ? "La mission est un succès !" : "La mission est un échec !";
        set => ResultMessage = value;
    }
}