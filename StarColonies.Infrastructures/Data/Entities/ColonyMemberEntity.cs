namespace StarColonies.Infrastructures.Data.Entities;

public class ColonyMemberEntity
{
    public int ColonyId { get; set; }
    public ColonyEntity Colony { get; set; }

    public string ColonistId { get; set; } = string.Empty;
    public ColonistEntity Colonist { get; set; }
}