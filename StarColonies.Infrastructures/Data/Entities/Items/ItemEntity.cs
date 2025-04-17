using StarColonies.Domains.Models.Items;

namespace StarColonies.Infrastructures.Data.Entities.Items;

public class ItemEntity
{
    public int Id { get; set; }
    public required string Name { get; set; } = "DEFAULT ITEM";
    public required string Description { get; set; } = "DEFAULT DESCRIPTION";
    
    public required int EffectId { get; set; }
    public required EffectEntity Effect { get; set; }
    
    public required int CoinsValue { get; set; }
    
    public required string ImagePath { get; set; } = "DEFAULT IMAGE PATH";
    
    public ICollection<RewardedEntity> Rewards { get; set; } = new List<RewardedEntity>();
}