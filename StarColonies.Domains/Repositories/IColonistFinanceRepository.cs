namespace StarColonies.Domains.Repositories;

public interface IColonistFinanceRepository
{
    Task DebitColonistAsync(string id, int amount);
    Task AddMustyColonistAsync(string id, int amount);
}