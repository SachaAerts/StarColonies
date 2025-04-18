using StarColonies.Domains.Models.Items;

namespace StarColonies.Domains.Services.CalculateService;

public class ItemsCalculationService : ICalculationService<IList<ItemModel>>
{
    public double CalculateStrength(IList<ItemModel> entity)
        => entity.Sum(item => item.Effect.ForceModifier ?? 0);

    public double CalculateStamina(IList<ItemModel> entity)
        => entity.Sum(item => item.Effect.StaminaModifier ?? 0);
}