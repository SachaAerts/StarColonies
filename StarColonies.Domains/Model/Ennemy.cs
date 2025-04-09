namespace StarColonies.Domains.Model;

public class Ennemy
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required int Strength { get; set; }
    public required int Stamina { get; set; }
    public required string ImagePath { get; set; }
}