namespace StarColonies.Domains.Models.Items;

public class ItemModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public required int CoinsValue { get; set; }
    
    public string ImagePath { get; set; } = string.Empty;
    
    public required EffectModel Effect { get; set; } = new();

    public int NumberOfBuy;
}