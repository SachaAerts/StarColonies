using StarColonies.Domains.Models;
using StarColonies.Domains.Models.Items;
using StarColonies.Infrastructures.Data.Entities.Items;

namespace StarColonies.Infrastructures.Data.Entities;

public class MissionEntity
{
    public required int Id { get; set; }
    public required int Difficulty { get; set; }
    public required string Name { get; set; } = "DEFAULT MISSION";
    
    public required int PlanetId { get; set; }
    public required PlanetEntity Planet { get; set; }
    
    public required int CoinsReward { get; set; }
    
    public required ICollection<RewardedEntity> Rewards { get; set; } = new List<RewardedEntity>();
    public required ICollection<EnemyEntity> Enemies { get; set; } = new List<EnemyEntity>();
}