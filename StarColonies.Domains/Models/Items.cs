namespace StarColonies.Domains.Models;

public class Items
{
    public required int Id { get; set; }
    public required string Name { get; set; } = "DEFAULT ITEM";
    public required string Description { get; set; } = "DEFAULT DESCRIPTION";
    
    public required int CoinsValue { get; set; } = 0;
    
    public required string ImagePath { get; set; } = "DEFAULT IMAGE PATH";
}