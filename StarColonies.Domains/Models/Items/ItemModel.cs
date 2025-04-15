namespace StarColonies.Domains.Models.Items;

public class ItemModel
{
    public required int Id { get; set; }
    public required string Name { get; set; } = "DEFAULT ITEM";
    public required string Description { get; set; } = "DEFAULT DESCRIPTION";
    
    public required int EffectId { get; set; }
    public required EffectModel Effect { get; set; } = new EffectModel();
    
    public required int CoinsValue { get; set; }
    
    public required string ImagePath { get; set; } = "DEFAULT IMAGE PATH";
    
    public required ICollection<RewardedModel> Rewards { get; set; } = new List<RewardedModel>();
}