namespace StarColonies.Domains.Models;

public class Mission
{
    public required int Id { get; set; }
    public required int Difficulty { get; set; }
    public required string Name { get; set; } = "DEFAULT MISSION";
    
    public required int PlanetId { get; set; }
    public required Planet Planet { get; set; }
    
    public required int CoinsReward { get; set; } = 0;
    public required ICollection<Items> ItemsToWin { get; set; } = new List<Items>();
    
    public required ICollection<Enemy> Enemies { get; set; } = new List<Enemy>();
}