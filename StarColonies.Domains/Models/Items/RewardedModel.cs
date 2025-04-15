namespace StarColonies.Domains.Models.Items;

public class RewardedModel
{
    public int MissionId { get; set; }
    public MissionModel Mission { get; set; }

    public int ItemId { get; set; }
    public ItemModel Item { get; set; }

    public int Quantity { get; set; } = 1;
}