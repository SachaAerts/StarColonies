namespace StarColonies.Domains.Models;

public class Planet
{
    public required int Id { get; set; }
    public required string Name { get; set; }

    public required string ImagePath { get; set; }
    public required ICollection<Mission> Missions { get; set; } = new List<Mission>();
}