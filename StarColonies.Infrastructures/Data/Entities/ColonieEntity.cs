using StarColonies.Infrastructures.Data.Entities.Missions;

namespace StarColonies.Infrastructures.Data.Entities;

public class ColonieEntity
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public required string OwnerId { get; set; }
    public required ColonistEntity Owner { get; set; }

    public ICollection<ColonieMemberEntity> Members { get; set; } = new List<ColonieMemberEntity>();

    public required ICollection<MissionExecutionEntity> MissionExecutions { get; set; } = new List<MissionExecutionEntity>();
}