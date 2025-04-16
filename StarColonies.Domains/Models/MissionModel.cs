using StarColonies.Domains.Models.Items;

namespace StarColonies.Domains.Models;

public class MissionModel
{
    public required int Id { get; set; }
    public required int Difficulty { get; set; }
    public required string Name { get; set; } = "DEFAULT MISSION";
    
    public required int PlanetId { get; set; }
    public required PlanetModel Planet { get; set; }
    
    public required int CoinsReward { get; set; }
    
    public required ICollection<ItemModel> Rewards { get; set; } = new List<ItemModel>();
    public required ICollection<EnemyModel> Enemies { get; set; } = new List<EnemyModel>();
}