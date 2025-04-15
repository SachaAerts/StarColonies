using StarColonies.Domains.Models;

namespace StarColonies.Infrastructures.Data.Entities;

public class PlanetEntity
{
    public required int Id { get; set; }
    public required string Name { get; set; }

    public required string ImagePath { get; set; }
    
    public required ICollection<MissionEntity> Missions { get; set; } = new List<MissionEntity>();
}