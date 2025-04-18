using StarColonies.Domains.Models.Items;

namespace StarColonies.Domains.Models.Colony;

public class ColonyModel
{
    public int Strength { get => Colonists.Sum(c => c.Strength + c.Level); }
    public int Stamina { get => Colonists.Sum(c => c.Stamina + c.Level); }
    
    IList<ColonistModel> Colonists { get; set; } = new List<ColonistModel>();
    List<ItemModel> Items { get; set; } = new();
}