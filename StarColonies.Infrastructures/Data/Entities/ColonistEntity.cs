using Microsoft.AspNetCore.Identity;
using StarColonies.Domains.Models;
using StarColonies.Infrastructures.Data.Entities.Items;

namespace StarColonies.Infrastructures.Data.Entities;

public class ColonistEntity : IdentityUser
{
    public required DateTime DateOfBirth { get; set; }
    
    public required JobModel JobModel { get; set; }
    
    public required int Level { get; set; }
    
    public required int Strength { get; set; }
    
    public required int Stamina { get; set; }
    
    public required int Musty { get; set; }
    
    public string ProfilPicture { get; set; }

    public ICollection<ColonieMemberEntity> Colonies { get; set; } = new List<ColonieMemberEntity>();
    public ICollection<InventoryEntity> Inventory { get; set; } = new List<InventoryEntity>();
}