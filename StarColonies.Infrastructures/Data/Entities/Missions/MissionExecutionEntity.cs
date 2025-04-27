namespace StarColonies.Infrastructures.Data.Entities.Missions;

public class MissionExecutionEntity
{
    public int Id { get; set; }

    public required int ColonyId { get; set; }
    public ColonyEntity Colony { get; set; }
    
    public required int PlanetId { get; set; }
    public PlanetEntity Planet { get; set; }

    public required int MissionId { get; set; }
    public MissionEntity Mission { get; set; }

    public DateTime ExecutedAt { get; set; } = DateTime.UtcNow;
    
    public required bool LivingColony { get; set; }
    public required bool OvercomingMission { get; set; }
    
    public required bool IsSuccess { get; set; }
    
    public required int RewardedCoins { get; set; }
}