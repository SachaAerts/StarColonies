namespace StarColonies.Domains.Models.Items;

public class ItemModel
{
    public string Name { get; set; }
    
    public required EffectModel Effect { get; set; } = new EffectModel();
    
    public required int CoinsValue { get; set; }
}