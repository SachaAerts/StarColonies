using Microsoft.Extensions.DependencyInjection;
using StarColonies.Domains.Models.Missions;

namespace StarColonies.Infrastructures.Services.RewardStrategy;

public class MissionRewardStrategyFactory(IServiceProvider serviceProvider) : IStrategyFactory
{
    private readonly IDictionary<MissionResultKey, Type> strategyMap = new Dictionary<MissionResultKey, Type>
    {
        [new MissionResultKey(true, true)]  = typeof(FullSuccessRewardStrategy),
        [new MissionResultKey(false, true)] = typeof(MoneyRewardStrategy),
        [new MissionResultKey(true, false)] = typeof(ResourceRewardStrategy)
    };
    
    public IMissionRewardStrategy GetStrategy(MissionResultModel result)
    {
        var key = new MissionResultKey(result.OvercomingMission, result.LivingColony);

        if (strategyMap.TryGetValue(key, out var strategyType)) //Essayer de trouver la clé dans le dictionnaire
            return (IMissionRewardStrategy)ActivatorUtilities.CreateInstance(serviceProvider, strategyType); //Créer une instance de la stratégie correspondante

        return new NoRewardStrategy();
    }
}

public record MissionResultKey(bool OvercomingMission, bool LivingColony);