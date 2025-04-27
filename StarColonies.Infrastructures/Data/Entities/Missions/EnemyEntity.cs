namespace StarColonies.Infrastructures.Data.Entities.Missions;

public class EnemyEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    
    public int TypeId { get; set; }
    public TypeEntity Type { get; set; }
    
    public required int Strength { get; set; }
    public required int Stamina { get; set; }
    
    public required string ImagePath { get; set; }
    
    public ICollection<MissionEntity> Missions { get; set; } = new List<MissionEntity>();
}
