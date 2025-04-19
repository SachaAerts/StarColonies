using StarColonies.Domains.Models.Missions;

namespace StarColonies.Domains.Services.CalculateService;

public class RandomMissionCalculationService : ICalculationService<MissionModel>
{
    private readonly Random _random = new();
    
    public double CalculateStrength(MissionModel mission) 
        => mission.Strength * GenerateMultiplier();

    public double CalculateStamina(MissionModel mission) 
        => mission.Stamina * GenerateMultiplier();
    
    private double GenerateMultiplier() 
        => _random.NextDouble() * (2.5 - 1.5) + 1.5;
}