namespace StarColonies.Infrastructures.Data.Entities.Missions;

public class MissionExecutionEntity
{
    public int Id { get; set; }

    public required int ColonieId { get; set; }
    public required ColonyEntity Colony { get; set; }

    public required int MissionId { get; set; }
    public required MissionEntity Mission { get; set; }

    public required DateTime ExecutedAt { get; set; } = DateTime.UtcNow;

    public required bool IsSuccess { get; set; }

    public required int RewardedCoins { get; set; }
}