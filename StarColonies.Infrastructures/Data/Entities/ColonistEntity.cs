using Microsoft.AspNetCore.Identity;
using StarColonies.Domains.Models;

namespace StarColonies.Infrastructures.Data.Entities;

public class ColonistEntity : IdentityUser
{
    public required DateTime DateOfBirth { get; set; }
    
    public required JobModel JobModel { get; set; }
    
    public required int Level { get; set; }
    
    public required int Strength { get; set; }
    
    public required int Endurance { get; set; }
    
    public required int Musty { get; set; }

    public ICollection<ColonieMemberEntity> Colonies { get; set; } = new List<ColonieMemberEntity>();
}
