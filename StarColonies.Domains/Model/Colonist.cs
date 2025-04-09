namespace StarColonies.Domains.Model;

public class Colonist
{
    public required int Id { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string HashedPassword { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public required Job Job { get; set; }
    public required int Level { get; set; }
    public required int Strength { get; set; }
    public required int Endurance { get; set; }
    public required int Musty { get; set; }
}