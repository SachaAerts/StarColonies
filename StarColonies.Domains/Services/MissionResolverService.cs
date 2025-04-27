using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Models.Missions;
using StarColonies.Domains.Services.CalculateService;

namespace StarColonies.Domains.Services;

public class MissionResolverService()
{
    private readonly ICalculationService<MissionModel> _missionCalculationService 
        = new RandomMissionCalculationService();
    
    private readonly ICalculationService<ColonyModel> _colonyCalculationService 
        = new RandomColonyCalculationService();
    
    private readonly ICalculationService<IList<ItemModel>> _itemCalculationService
        = new ItemsCalculationService();
    
    public MissionResultModel Result(MissionModel mission, ColonyModel colony, List<ItemModel?> items)
    {
        double itemStrengthSum = _itemCalculationService.CalculateStrength(items),
               itemStaminaSum  = _itemCalculationService.CalculateStamina(items);
        
        double missionStrength = _missionCalculationService.CalculateStrength(mission),
               missionStamina  = _missionCalculationService.CalculateStamina(mission);

        double colonyStrength  = _colonyCalculationService.CalculateStrength(colony) + itemStrengthSum, 
               colonyStamina   = _colonyCalculationService.CalculateStamina(colony) + itemStaminaSum;

        return new MissionResultModel()
        {
            OvercomingMission = colonyStrength > missionStrength,
            LivingColony  = colonyStamina > missionStamina,
            CoinsReward = mission.CoinsReward,
            Rewards = mission.Items
        };
    }
}