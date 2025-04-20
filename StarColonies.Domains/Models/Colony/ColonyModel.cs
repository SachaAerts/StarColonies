using StarColonies.Domains.Models.Items;

namespace StarColonies.Domains.Models.Colony;

public class ColonyModel
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    
    public DateTime CreatedAt { get; set; }
    public string OwnerId { get; set; } = "";
    
    public string LogoPath { get; set; } = "";
    
    public int Strength => Colonists.Sum(c => c.Strength + c.Level);
    public int Stamina => Colonists.Sum(c => c.Stamina + c.Level);
    
    IList<ColonistModel> Colonists { get; set; } = new List<ColonistModel>();

    private List<ItemModel> Items { get; set; } = new();
}