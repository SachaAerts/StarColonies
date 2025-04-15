namespace StarColonies.Infrastructures.Data.Entities;

public class EnemyType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
    
    public ICollection<EnemyEntity> Enemies { get; set; } = new List<EnemyEntity>();
}