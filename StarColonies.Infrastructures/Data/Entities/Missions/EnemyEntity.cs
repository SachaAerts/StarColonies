using StarColonies.Domains.Models;

namespace StarColonies.Infrastructures.Data.Entities;

public class EnemyEntity
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    
    public int TypeId { get; set; }
    public required TypeEntity Type { get; set; }
    
    public required int Strength { get; set; }
    public required int Stamina { get; set; }
    
    public required string ImagePath { get; set; }
    
    public required ICollection<MissionEntity> Missions { get; set; } = new List<MissionEntity>();
}
