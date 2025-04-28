namespace StarColonies.Domains.Repositories;

public interface IAuthenticationRepository
{
    Task<(bool Success, string? ErrorMessage)> SignInAsync(string emailOrUsername, string password);
}