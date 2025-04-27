using StarColonies.Domains.Models;
using StarColonies.Infrastructures.Data.Entities.Missions;

namespace StarColonies.Infrastructures.Data.Entities.Items;

public class RewardedEntity
{
    public int MissionId { get; set; }
    public MissionEntity Mission { get; set; }

    public required int ItemId { get; set; }
    public ItemEntity Item { get; set; }

    public required int Quantity { get; set; } = 1;
}