using StarColonies.Domains.Models.Missions;

namespace StarColonies.Domains.Services.CalculateService;

public interface ICalculationService<T>
{
    double CalculateStrength(T entity);
    double CalculateStamina(T entity);
}