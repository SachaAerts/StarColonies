namespace StarColonies.Domains.Models;

public class Planet
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string ImagePath { get; set; }
}