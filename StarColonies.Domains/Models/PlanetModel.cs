namespace StarColonies.Domains.Models;

public class PlanetModel
{
    public required int Id { get; set; }
    public required string Name { get; set; }

    public required string ImagePath { get; set; }
    public required ICollection<MissionModel> Missions { get; set; } = new List<MissionModel>();
}