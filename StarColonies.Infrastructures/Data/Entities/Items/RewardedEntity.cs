using StarColonies.Domains.Models;
using StarColonies.Infrastructures.Data.Entities.Missions;

namespace StarColonies.Infrastructures.Data.Entities.Items;

public class RewardedEntity
{
    public required int MissionId { get; set; }
    public required MissionEntity Mission { get; set; }

    public required int ItemId { get; set; }
    public required ItemEntity Item { get; set; }

    public required int Quantity { get; set; } = 1;
}