namespace StarColonies.Domains.Models.Items;

public class EffectModel
{
    public int Id { get; set; }

    public string Name { get; set; } = "DEFAULT EFFECT";

    public int? ForceModifier { get; set; } 
    public int? StaminaModifier { get; set; }

    public ICollection<ItemModel> Items { get; set; } = new List<ItemModel>();
}
