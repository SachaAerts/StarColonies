using Microsoft.AspNetCore.Identity;
using StarColonies.Domains.Repositories;
using StarColonies.Infrastructures.Data.Entities;

namespace StarColonies.Infrastructures.Repositories;

public class AuthenticationRepository(
    SignInManager<ColonistEntity> signInManager, 
    UserManager<ColonistEntity> userManager) : IAuthenticationRepository
{
    public async Task<(bool Success, string? ErrorMessage)> SignInAsync(string emailOrUsername, string password)
    {
        var user = await userManager.FindByEmailAsync(emailOrUsername)
                   ?? await userManager.FindByNameAsync(emailOrUsername);

        if (user == null) return (false, "User not found");

        var result = await signInManager.PasswordSignInAsync(user, password, isPersistent: false, lockoutOnFailure: false);

        return result.Succeeded ? (true, null) : (false, "Wrong password");
    }
}