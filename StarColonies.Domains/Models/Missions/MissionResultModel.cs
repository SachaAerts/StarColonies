
namespace StarColonies.Domains.Models.Missions;

public class MissionResultModel
{
    public bool OvercomingMission { get; set; }
    public bool LivingColony { get; set; }
    
    public bool MissionSuccess => OvercomingMission && LivingColony;
    
    public string ResultMessage
    {
        get => MissionSuccess ? "La mission est un succès !" : 
            LivingColony ? "La mission a échoué, mais la colonie est toujours en vie." : 
                           "La mission a échoué et la colonie est morte.";
        set => ResultMessage = value;
    }
}