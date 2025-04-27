using StarColonies.Domains.Models;

namespace StarColonies.Infrastructures.Data.Seeder.Colonist;

public class Players
{
   public static IEnumerable<Player> GetAll()
        => new List<Player>
            {
                new ("AlexStriker",    "alex.striker@starcolonies.com",     JobModel.Engineer),
                new ("MiraNova",       "mira.nova@starcolonies.com",        JobModel.Doctor),
                new ("ElaraStarfinder","elara.starfinder@starcolonies.com", JobModel.Scientist),
                new ("JasonBlades",    "jason.blades@starcolonies.com",     JobModel.Soldier),
                new ("LyraVega",       "lyra.vega@starcolonies.com",        JobModel.Engineer),
                new ("OrionSkye",      "orion.skye@starcolonies.com",       JobModel.Scientist),
                new ("SeleneFrost",    "selene.frost@starcolonies.com",     JobModel.Doctor),
                new ("KaiHunter",      "kai.hunter@starcolonies.com",       JobModel.Soldier),
                new ("NovaRyder",      "nova.ryder@starcolonies.com",       JobModel.Engineer),
                new ("DaxValor",       "dax.valor@starcolonies.com",        JobModel.Soldier),
                new ("IvyQuinn",       "ivy.quinn@starcolonies.com",        JobModel.Scientist),
                new ("ZaneShadow",     "zane.shadow@starcolonies.com",      JobModel.Soldier),
                new ("LunaDrake",      "luna.drake@starcolonies.com",       JobModel.Doctor),
                new ("AxelStorm",      "axel.storm@starcolonies.com",       JobModel.Engineer),
                new ("VeraCross",      "vera.cross@starcolonies.com",       JobModel.Scientist),
                new ("JettRaven",      "jett.raven@starcolonies.com",       JobModel.Soldier),
                new ("NiaFalcon",      "nia.falcon@starcolonies.com",       JobModel.Engineer),
                new ("CaiusHolt",      "caius.holt@starcolonies.com",       JobModel.Scientist),
                new ("SoraVance",      "sora.vance@starcolonies.com",       JobModel.Doctor),
                new ("RexOrion",       "rex.orion@starcolonies.com",        JobModel.Soldier)
            };
}

public class Player(string userName, string email, JobModel job)
{
    public string UserName { get; } = userName;
    public string Email { get; } = email;
    public JobModel Job { get; } = job;
}