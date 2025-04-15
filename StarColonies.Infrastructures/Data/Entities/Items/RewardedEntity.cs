namespace StarColonies.Domains.Models.Items;

public class RewardedEntity
{
    public int MissionId { get; set; }
    public MissionModel Mission { get; set; }

    public int ItemId { get; set; }
    public ItemEntity Item { get; set; }

    public int Quantity { get; set; } = 1;
}