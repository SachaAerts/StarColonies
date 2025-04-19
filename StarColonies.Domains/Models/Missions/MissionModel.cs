using StarColonies.Domains.Models.Items;

namespace StarColonies.Domains.Models.Missions;

public class MissionModel
{
    public int Id { get; set; }
    public int Difficulty { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public int CoinsReward { get; set; }
    
    public IList<EnemyModel> Enemies { get; set; } = new List<EnemyModel>();
    public IList<ItemModel> Items { get; set; } = new List<ItemModel>();

    public int Strength => Enemies.Sum(e => e.Strength);
    public int Stamina => Enemies.Sum(e => e.Stamina);
}
