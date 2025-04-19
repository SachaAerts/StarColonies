using StarColonies.Domains.Models.Colony;

namespace StarColonies.Domains.Services.CalculateService;

public class RandomColonyCalculationService : ICalculationService<ColonyModel>
{
    private readonly Random _random = new();

    public double CalculateStrength(ColonyModel colony)
        => colony.Strength * GenerateMultiplier();

    public double CalculateStamina(ColonyModel colony)
        => colony.Stamina * GenerateMultiplier();

    private double GenerateMultiplier()
        => _random.NextDouble() * (2.5 - 1.5) + 1.5;
}