namespace StarColonies.Domains.Models.Items;

public class EffectModel
{
    public int? ForceModifier { get; set; } 
    public int? StaminaModifier { get; set; }

    public ICollection<ItemModel> Items { get; set; } = new List<ItemModel>();
}
