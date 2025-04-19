namespace StarColonies.Domains.Models.Missions;

public class EnemyModel
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Type { get; set; } = "";
    
    public int Strength { get; set; }
    public int Stamina { get; set; }
    
    public string ImagePath { get; set; } = "";
}
