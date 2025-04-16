using StarColonies.Domains.Models.Items;

namespace StarColonies.Infrastructures.Data.Entities.Items;

public class EffectEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = "DEFAULT EFFECT";

    public int? ForceModifier { get; set; } 
    public int? StaminaModifier { get; set; }

    public ICollection<ItemEntity> Items { get; set; } = new List<ItemEntity>();
}
