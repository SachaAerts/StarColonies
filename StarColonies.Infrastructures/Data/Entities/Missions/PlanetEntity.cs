namespace StarColonies.Infrastructures.Data.Entities.Missions;

public class PlanetEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public required int X { get; set; }
    public required int Y { get; set; }

    public required string ImagePath { get; set; }
    
    public ICollection<MissionEntity> Missions { get; set; } = new List<MissionEntity>();
}