namespace StarColonies.Domains.Models.Missions;

public class MissionModel
{
    public IList<EnemyModel> Enemies { get; set; } = new List<EnemyModel>();

    public int Strength => Enemies.Sum(e => e.Strength);
    public int Stamina => Enemies.Sum(e => e.Stamina);
}