using StarColonies.Infrastructures.Data.Entities.Missions;

namespace StarColonies.Infrastructures.Data.Entities;

public class TypeEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public ICollection<EnemyEntity> Enemies { get; set; } = new List<EnemyEntity>();
}