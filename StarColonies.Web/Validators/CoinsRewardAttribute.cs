using System.ComponentModel.DataAnnotations;

namespace StarColonies.Web.Validators;

public class CoinsRewardAttribute : ValidationAttribute
{
    public int Max { get; set; } = 1000;
    public int Min { get; set; } = 1;

    public override bool IsValid(object? value)
    {
        if (value is not int coins) return true;

        if (coins >= Min && coins <= Max) return true;
        ErrorMessage = $"You can select between {Min} and {Max} coins.";
        return false;
    }
}