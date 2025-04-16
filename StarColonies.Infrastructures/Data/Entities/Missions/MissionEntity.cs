using StarColonies.Infrastructures.Data.Entities.Items;

namespace StarColonies.Infrastructures.Data.Entities.Missions;

public class MissionEntity
{
    public int Id { get; set; }
    public required int Difficulty { get; set; }
    public required string Name { get; set; } = "DEFAULT MISSION";
    
    public required string Description { get; set; } = "DEFAULT DESCRIPTION";
    
    public required int PlanetId { get; set; }
    public required PlanetEntity Planet { get; set; }
    
    public required int CoinsReward { get; set; }
    
    public ICollection<RewardedEntity> Rewards { get; set; } = new List<RewardedEntity>();
    public ICollection<EnemyEntity> Enemies { get; set; } = new List<EnemyEntity>();
}