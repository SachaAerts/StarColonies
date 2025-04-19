namespace StarColonies.Domains.Models.Colony;

public class ColonistModel
{
    public required string Id { get; set; }
    public required string? Name { get; set; }
    public required string? Email { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public required string ProfilPicture { get; set; }
    
    public required int Level { get; set; }
    
    public required int Strength { get; set; }
    public required int Stamina { get; set; }
    
    public required int Musty { get; set; }
    
    public JobModel Job { get; set; }
}
