namespace StarColonies.Domains.Models;

public class EnemyModel
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    
    public required TypeModel EnemyTypeModel { get; set; }
    
    public required int Strength { get; set; }
    public required int Stamina { get; set; }
    
    public required string ImagePath { get; set; }
    
    public required ICollection<MissionModel> Missions { get; set; } = new List<MissionModel>();
}
