using Microsoft.AspNetCore.Identity;
using StarColonies.Infrastructures.Data.Entities;

namespace StarColonies.Infrastructures.Data.Seeder.Colonist;

public interface IUserSeeder
{
    Task SeedAsync(UserManager<ColonistEntity> userManager, RoleManager<IdentityRole> roleManager);
}