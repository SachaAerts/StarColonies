using System.ComponentModel.DataAnnotations;
using StarColonies.Web.Pages;

namespace StarColonies.Web.Validators;

public class MaxEnemiesAttribute : ValidationAttribute
{
    public int Max { get; set; } = 3;

    public override bool IsValid(object? value)
    {
        if (value is not List<int> list) return true;

        if (list.Count <= Max) return true;
        ErrorMessage = $"Vous pouvez sélectionner au maximum {Max} ennemis.";
        return false;

    }
}