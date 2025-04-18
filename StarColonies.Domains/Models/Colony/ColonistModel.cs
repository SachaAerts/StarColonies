namespace StarColonies.Domains.Models.Colony;

public class ColonistModel
{
    public required int Level { get; set; }
    
    public required int Strength { get; set; }
    public required int Stamina { get; set; }
    
    public required int Musty { get; set; }
    
    public JobModel Job { get; set; }
}
