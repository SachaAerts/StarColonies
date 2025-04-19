namespace StarColonies.Infrastructures.Data.Entities;

public class ColonieMemberEntity
{
    public int ColonieId { get; set; }
    public required ColonyEntity Colony { get; set; }

    public string ColonistId { get; set; } = string.Empty;
    public required ColonistEntity Colonist { get; set; }
}