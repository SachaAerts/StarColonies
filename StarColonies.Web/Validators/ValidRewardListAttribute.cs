using System.ComponentModel.DataAnnotations;
using StarColonies.Web.Pages;

namespace StarColonies.Web.Validators;

public class ValidRewardListAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is not List<RewardInput> rewards)
            return true;

        if (!rewards.Any(r => r.Selected))
        {
            ErrorMessage = "Vous devez sélectionner au moins un objet de récompense.";
            return false;
        }

        if (!rewards.Any(r => r is { Selected: true, Quantity: <= 0 })) return true;
        ErrorMessage = "Les objets sélectionnés doivent avoir une quantité strictement positive.";
        return false;

    }
}