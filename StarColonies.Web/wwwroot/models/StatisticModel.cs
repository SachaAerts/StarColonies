namespace StarColonies.Web.wwwroot.models;

public class StatisticModel
{
    public IList<string> ItemsLabel { get; set; } = new List<string>();
    public IList<int> NumberOfBuysPerItems { get; set; } = new List<int>();
    public IList<string> Top10ColonyLabels { get; set; } = new List<string>();
    public IList<int> Top10ColonyStrength { get; set; } = new List<int>();
    public IList<int> Top10ColonyStamina { get; set; } = new List<int>();
    public IList<string> PlanetsLabel { get; set; } = new List<string>();
    public IList<int> MissionsSucceed { get; set; } = new List<int>();
    public IList<int> MissionsFailed { get; set; } = new List<int>();
}