using StarColonies.Domains.Models.Missions;

namespace StarColonies.Domains.Models;

public class PlanetModel
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int X { get; set; }
    public int Y { get; set; }
    public string ImagePath { get; set; } = "";

    public List<MissionModel> Missions { get; set; } = [];
}